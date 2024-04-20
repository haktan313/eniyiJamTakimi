using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) {
            TriggerDialouge();
        }
    }

    public void TriggerDialouge()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
