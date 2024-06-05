using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LieBossController : MonoBehaviour
{
    public LieBoss lieBoss;
    CameraShake cameraShake;

    public float bossHp = 100;
    public float playerHp = 100;
    bool isAnswered = false;

    [SerializeField] string[] attackSentences;

    [SerializeField] GameObject buttons;
    [SerializeField] Button correctButton;
    [SerializeField] Button wrongButton;
    [SerializeField] Button wrongButton2;
    [SerializeField] Button wrongButton3;
    [SerializeField] Button[] wrongButtons = new Button[3];

    public SlowMotionController slowMotionController;

    public GameObject darkenScreenImage;
    public float darkenDuration = 2.0f;
    public Image darkenImageComponent;
    private Color targetColor;

    [SerializeField] BP_Health bossHealth;
    [SerializeField] BP_Health playerHealth;

    [SerializeField] Animator animator;
    bool isAttacking = false;
    private void Start()
    {
        darkenScreenImage.SetActive(false);
        wrongButtons[0] = wrongButton;
        wrongButtons[1] = wrongButton2;
        wrongButtons[2] = wrongButton3;

        cameraShake = GetComponent<CameraShake>();


        correctButton.onClick.AddListener(CorrectButtonClick);
        wrongButton.onClick.AddListener(WrongButtonClick);
        wrongButton2.onClick.AddListener(WrongButtonClick);
        wrongButton3.onClick.AddListener(WrongButtonClick);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            //cameraShake.Shake();
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        for (int i = 0; i < attackSentences.Length; i++)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", isAttacking);
            isAnswered = false; 
            lieBoss.StartSpeech(attackSentences[i]);
            yield return new WaitUntil(() => lieBoss.isArrived);
            isAttacking = false;
            animator.SetBool("isAttacking", isAttacking);
            StartTest(i);
            slowMotionController.StartSlowMotion();
            yield return new WaitUntil(() => isAnswered);
            slowMotionController.ResetTimeScale();

            yield return new WaitForSeconds(3f);

            darkenScreenImage.SetActive(true);
            yield return StartCoroutine(FadeImage(true, darkenDuration));
            yield return new WaitForSeconds(darkenDuration);
            yield return StartCoroutine(FadeImage(false, darkenDuration));
            darkenScreenImage.SetActive(false);

            yield return new WaitForSeconds(4f);
        }
    }

    void StartTest(int sentenceIndex)
    {
        Debug.Log("SEX");
        buttons.SetActive(true);
        switch (sentenceIndex)
        {
            case 0:
                correctButton.GetComponentInChildren<TextMeshProUGUI>().text = "Tuborg Red";
                wrongButton.GetComponentInChildren<TextMeshProUGUI>().text = "Bomonti";
                wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = "Bremen";
                wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = "Efes";
                break;
            case 1:
                SwapButtons(correctButton, wrongButton);
                correctButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pandora";
                wrongButton.GetComponentInChildren<TextMeshProUGUI>().text = "Gucci";
                wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = "Vivien";
                wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = "Cikomokoko";
                break;
            case 2:
                SwapButtons(correctButton, wrongButton2);
                correctButton.GetComponentInChildren<TextMeshProUGUI>().text = "Arabesk";
                wrongButton.GetComponentInChildren<TextMeshProUGUI>().text = "Rock";
                wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = "Rap";
                wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = "Jazz";
                break;
            case 3:
                SwapButtons(correctButton, wrongButton3);
                correctButton.GetComponentInChildren<TextMeshProUGUI>().text = "Rottweiller";
                wrongButton.GetComponentInChildren<TextMeshProUGUI>().text = "Golden";
                wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = "Terrier";
                wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = "Kangal";
                break;
            case 4:
                SwapButtons(correctButton, wrongButton);
                correctButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sari";
                wrongButton.GetComponentInChildren<TextMeshProUGUI>().text = "Esmer";
                wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = "Kahverengi";
                wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = "Siyah";
                break;
        }
    }

    public void CorrectButtonClick()
    {
        bossHealth.currentHealth -= 20;
        Debug.Log("Boss HP : " + bossHealth.currentHealth);
        buttons.SetActive(false);
        isAnswered = true;
        if(bossHealth.currentHealth <= 0)
        {
            SceneManager.LoadScene(8);
        }
    }

    public void WrongButtonClick()
    {
        playerHealth.currentHealth -= 20;
        Debug.Log("Player Hp : " +  playerHp);
        buttons.SetActive(false);
        isAnswered = true;
        if(playerHealth.currentHealth <= 0)
        {
            SceneManager.LoadScene(7);
        } 
    }


    void SwapButtons(Button button1, Button button2)
    {
        Vector3 tempPosition = button1.transform.position;
        button1.transform.position = button2.transform.position;
        button2.transform.position = tempPosition;
    }


    private IEnumerator FadeImage(bool fadeIn, float duration)
    {
        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            targetColor = darkenImageComponent.color;
            targetColor.a = alpha;
            darkenImageComponent.color = targetColor;
            yield return null;
        }

        targetColor.a = endAlpha;
        darkenImageComponent.color = targetColor;
    }


}
