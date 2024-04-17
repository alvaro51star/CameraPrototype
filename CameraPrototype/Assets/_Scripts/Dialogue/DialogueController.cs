using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    //[SerializeField] private AudioSource audioSource;
    [Header("Text variables")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingTime; //con 0.05s son 20 char/s
    [SerializeField] private float nexLineTime;

    public static DialogueController instance;
    public GameObject dialogueGameObject;
    public bool didDialogueStart;
   
    private int lineIndex;
    private string[] dialogueLines;
    private GameObject activeTrigger;
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
        activeTrigger = trigger;
        /*
        uiManager.IsInGame(false); // parar interacciones
        uiManager.DesactivateAllUIGameObjects(); //desactivar UI (ya lo ha hecho miriam)
        SoundManager.instance.ReproduceSound(AudioClipsNames.Pop, audioSource);
        uiManager.ActivateUIGameObjects(uiManager.dialoguePanel, true); //activar ui dialogo
        
        uiManager.dialoguePanel.SetActive(true);*/

        uiManager.dialoguePanel.SetActive(true);

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
        uiManager.dialoguePanel.SetActive(false);
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
        else if (lineIndex > dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            EndLineFast();
        }
    }
    private void EndLineFast()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueLines[lineIndex];
    }
}
