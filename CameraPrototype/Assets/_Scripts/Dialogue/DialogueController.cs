using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    //[SerializeField] private UIManager uiManager;
    //[SerializeField] private AudioSource audioSource;
    [Header("Text variables")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingTime; //con 0.05s son 20 char/s
    [SerializeField] private float nexLineTime;
    [SerializeField] private Animator elevatorAnimator;
    [SerializeField] private Animator elevatorAnimator2;

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

    public void StartDialogue(string[] textLines, GameObject trigger)
    {
        //activeTrigger = trigger;
        EventManager.OnIsReading?.Invoke();
        /*
        uiManager.IsInGame(false); // parar interacciones
        uiManager.DesactivateAllUIGameObjects(); //desactivar UI (ya lo ha hecho miriam)
        SoundManager.instance.ReproduceSound(AudioClipsNames.Pop, audioSource);
        uiManager.ActivateUIGameObjects(uiManager.dialoguePanel, true); //activar ui dialogo
        
        uiManager.dialoguePanel.SetActive(true);*/

        UIManager.instance.dialoguePanel.SetActive(true);
        UIManager.instance.SetIsGamePaused(true);
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
        /*
        uiManager.DesactivateAllUIGameObjects(); //desactivar ui dialogo
        uiManager.IsInGame(true); //meter interaccion
        */
        UIManager.instance.dialoguePanel.SetActive(false);
        UIManager.instance.SetIsGamePaused(false);
        UIManager.instance.SetPointersActive(true);
        UIManager.instance.SetIsReading(false);
        UIManager.instance.m_isInDialogue = false;

        EventManager.OnStopReading?.Invoke();
        elevatorAnimator.enabled = true;
        elevatorAnimator2.enabled = true;
        this.enabled = false;
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
        NextDialogueLine();
    }

    public void NextDialogueLine()
    {
        //SoundManager.instance.ReproduceSound(AudioClipsNames.Click, audioSource);

        if (dialogueText.text == dialogueLines[lineIndex]) //si enseña la linea completa
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
