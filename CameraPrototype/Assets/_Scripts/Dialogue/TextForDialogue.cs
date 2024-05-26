using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextForDialogue : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;

    [TextArea(2, 4)] public string[] textLines;// 2 = minNumLineas, 6 = maxNumLineas
    
    public void StartDialogue()
    {
        dialogueController.StartDialogue(textLines);
    }
}
