using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    private void Start()
    {
        darkenScreenImage.SetActive(false);
        wrongButtons[0] = wrongButton;
        wrongButtons[1] = wrongButton2;
        wrongButtons[2] = wrongButton3;

        cameraShake = GetComponent<CameraShake>();

        correctButton.onClick.AddListener(CorrectButtonClick);
        for (int i = 0; i < wrongButtons.Length; i++)
        {
            wrongButtons[i].onClick.AddListener(WrongButtonClick);
        }

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
            isAnswered = false; 

            lieBoss.StartSpeech(attackSentences[i]);
            yield return new WaitUntil(() => lieBoss.isArrived);
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

            yield return new WaitForSeconds(10f);
        }
    }

    void StartTest(int sentenceIndex)
    {
        Debug.Log("SEX");
        buttons.SetActive(true);
        switch (sentenceIndex)
        {
            case 0:
                correctButton.GetComponentInChildren<TextMeshProUGUI>().text = " 31 ";
                wrongButton.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                break;
            case 1:
                SwapButtons(correctButton, wrongButton);
                correctButton.GetComponentInChildren<TextMeshProUGUI>().text = " 32 ";
                wrongButton.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                break;
            case 2:
                SwapButtons(correctButton, wrongButton2);
                correctButton.GetComponentInChildren<TextMeshProUGUI>().text = " 33 ";
                wrongButton.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                break;
            case 3:
                SwapButtons(correctButton, wrongButton3);
                correctButton.GetComponentInChildren<TextMeshProUGUI>().text = " 34 ";
                wrongButton.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                break;
            case 4:
                SwapButtons(correctButton, wrongButton);
                correctButton.GetComponentInChildren<TextMeshProUGUI>().text = " 35 ";
                wrongButton.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                wrongButton2.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                wrongButton3.GetComponentInChildren<TextMeshProUGUI>().text = " ";
                break;
        }
    }

    public void CorrectButtonClick()
    {
        bossHp = 100;
        bossHp -= 20f;
        Debug.Log("Boss HP : " + bossHp);
        buttons.SetActive(false);
        isAnswered = true;
    }

    public void WrongButtonClick()
    {
        playerHp -= 100;
        Debug.Log("Player Hp : " +  playerHp);
        buttons.SetActive(false);
        isAnswered = true;
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
