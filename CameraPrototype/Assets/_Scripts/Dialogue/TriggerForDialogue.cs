using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class TriggerForDialogue : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private bool isInteractive;

    [TextArea(2, 4)] public string[] textLines;// 2 = minNumLineas, 6 = maxNumLineas

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!dialogueController.didDialogueStart && !isInteractive)
            {
                dialogueController.StartDialogue(textLines, isInteractive, gameObject);
                Debug.Log("Start dialogue");
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && !dialogueController.didDialogueStart && isInteractive)
        {
            dialogueController.StartDialogue(textLines, isInteractive, gameObject);
        }
    }
}
