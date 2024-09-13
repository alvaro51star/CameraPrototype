using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    //[SerializeField] private AudioSource audioSource;
    [Header("Text variables")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingTime; //con 0.05s son 20 char/s
    [SerializeField] private float nexLineTime;
    [Header("Tutorial")]
    [SerializeField] private Animator elevatorAnimator;
    [SerializeField] private Animator elevatorAnimator2;
    [SerializeField] private GameObject controlsGO;

    public static DialogueController instance;
    public GameObject dialogueGameObject;
    public bool didDialogueStart;
   
    private int lineIndex;
    private string[] dialogueLines;
    //private GameObject activeTrigger;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(string[] textLines)
    {
        EventManager.OnIsReading?.Invoke();      

        UIManager.instance.dialoguePanel.SetActive(true);
        MenuButtons.instance.SetIsGamePaused(true);
        UIManager.instance.SetPointersActive(false);
        UIManager.instance.SetInteractionText(false, "");
        UIManager.instance.SetIsReading(true);
        UIManager.instance.m_isInDialogue = true;

        didDialogueStart = true;
        dialogueLines = textLines;
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    public void EndDialogue()
    {
        didDialogueStart = false;

        UIManager.instance.dialoguePanel.SetActive(false);
        MenuButtons.instance.SetIsGamePaused(false);
        UIManager.instance.SetPointersActive(true);
        UIManager.instance.SetIsReading(false);
        UIManager.instance.m_isInDialogue = false;

        EventManager.OnStopReading?.Invoke();
        if (!elevatorAnimator.isActiveAndEnabled)
        {
            elevatorAnimator.enabled = true;
            elevatorAnimator2.enabled = true;

            if(controlsGO.activeSelf)
                controlsGO.SetActive(false);
        }
        //this.enabled = false;
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        yield return new WaitForSecondsRealtime(nexLineTime);
        //NextDialogueLine();
    }

    public void NextDialogueLine()
    {
        //SoundManager.instance.ReproduceSound(AudioClipsNames.Click, audioSource);

        if(dialogueText.text == string.Empty)
            return; 

        if (dialogueText.text == dialogueLines[lineIndex]) //si ense�a la linea completa
        {
            lineIndex++;
            if (lineIndex < dialogueLines.Length)
            {
                StartCoroutine(ShowLine());
            }
            else
            {
                EndDialogue();
            }
        }
        else
        {
            if (lineIndex > dialogueLines.Length)
            {
                EndDialogue();
                return;
            }
            EndLineFast();
        }
    }

    private void EndLineFast()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueLines[lineIndex];
    }
}
