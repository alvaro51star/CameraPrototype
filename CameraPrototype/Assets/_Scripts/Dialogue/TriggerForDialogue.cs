using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider))]

public class TriggerForDialogue : MonoBehaviour
{
    [FormerlySerializedAs("dialogueController")] [SerializeField] private DialogueController m_dialogueController;

    [TextArea(2, 4)] public string[] textLines;// 2 = minNumLineas, 6 = maxNumLineas

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!m_dialogueController.iDidDialogueStart)
            {
                m_dialogueController.StartDialogue(textLines);                
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
