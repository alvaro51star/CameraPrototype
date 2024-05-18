using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class TriggerForDialogue : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;

    [TextArea(2, 4)] public string[] textLines;// 2 = minNumLineas, 6 = maxNumLineas

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!dialogueController.didDialogueStart)
            {
                dialogueController.StartDialogue(textLines, gameObject);                
            }
        }
    }

    private void OnTriggerExit(Collider other) //para que no vuelva a saltar el trigger  
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
            this.gameObject.SetActive(false);
            this.enabled = false;
        }
    }
}
