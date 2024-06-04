using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LieBoss : MonoBehaviour
{
    public TextMeshProUGUI speechText; 
    public Transform player; 
    public float moveSpeed = 5f; 

    public bool isArrived = false;
    private bool isMoving = false;
    int randomNumber;

    [SerializeField] GameObject[] questionPoints;
    private Transform targetPoint;

    void Start()
    {
        speechText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isMoving)
        { 
            speechText.transform.position = Vector3.MoveTowards(speechText.transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(speechText.transform.position, player.position) < 2f)
            {
                isMoving = false;
                isArrived = true;
                speechText.gameObject.SetActive(false); 
            }
        }
    }

    public void StartSpeech(string sentence)
    {
        speechText.text = sentence;
        speechText.transform.position = transform.position; 
        speechText.gameObject.SetActive(true);

        randomNumber = Random.Range(0, questionPoints.Length);
        targetPoint = questionPoints[randomNumber].transform;

        isMoving = true; 
        isArrived = false;
    }

}
