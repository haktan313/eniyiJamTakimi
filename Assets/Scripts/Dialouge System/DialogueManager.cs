using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;


    private Queue<string> sentences;

    [SerializeField]
    private Dialogue dialogue;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        nameText.text = dialogue.name1;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(); 
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }

        if(nameText.text == dialogue.name1)
        {
            nameText.text = dialogue.name2;
        }
        else
        {
            nameText.text = dialogue.name1;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        switch(sceneIndex)
        {
            case 2:
                SceneManager.LoadScene(3);
                break;
            case 4:
                SceneManager.LoadScene(5);
                break;
            case 6:
                SceneManager.LoadScene(7);
                break;
        }
    }


}
