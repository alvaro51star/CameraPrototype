using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class DialogueController : MonoBehaviour
{
    //[SerializeField] private AudioSource audioSource;
    [Header("Text variables")]
    [FormerlySerializedAs("dialogueText")] [SerializeField] private TMP_Text m_TMPTxt_dialogueText;
    [FormerlySerializedAs("typingTime")] [SerializeField] private float m_typingTime; //con 0.05s son 20 char/s
    [FormerlySerializedAs("nexLineTime")] [SerializeField] private float m_nexLineTime;
    [Header("Tutorial")]
    [FormerlySerializedAs("elevatorAnimator")] [SerializeField] private Animator m_Animtr_elevatorAnimator;
    [FormerlySerializedAs("elevatorAnimator2")] [SerializeField] private Animator m_Animtr_elevatorAnimator2;
    [FormerlySerializedAs("controlsGO")] [SerializeField] private GameObject m_GO_controls;

    public static DialogueController instance;
    //public GameObject dialogueGameObject;
    [FormerlySerializedAs("didDialogueStart")] public bool iDidDialogueStart;
   
    private int m_lineIndex;
    private string[] m_str_dialogueLines;
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

        UIManager.instance.go_DialoguePanel.SetActive(true);
        MenuButtons.instance.SetIsGamePaused(true);
        UIManager.instance.SetPointersActive(false);
        UIManager.instance.SetInteractionText(false, "");
        UIManager.instance.SetIsReading(true);
        UIManager.instance.isInDialogue = true;

        iDidDialogueStart = true;
        m_str_dialogueLines = textLines;
        m_lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    public void EndDialogue()
    {
        iDidDialogueStart = false;

        UIManager.instance.go_DialoguePanel.SetActive(false);
        MenuButtons.instance.SetIsGamePaused(false);
        UIManager.instance.SetPointersActive(true);
        UIManager.instance.SetIsReading(false);
        UIManager.instance.isInDialogue = false;

        EventManager.OnStopReading?.Invoke();
        if (!m_Animtr_elevatorAnimator.isActiveAndEnabled)
        {
            m_Animtr_elevatorAnimator.enabled = true;
            m_Animtr_elevatorAnimator2.enabled = true;

            if(m_GO_controls.activeSelf)
                m_GO_controls.SetActive(false);
        }
        //this.enabled = false;
    }

    private IEnumerator ShowLine()
    {
        m_TMPTxt_dialogueText.text = string.Empty;
        foreach (char ch in m_str_dialogueLines[m_lineIndex])
        {
            m_TMPTxt_dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(m_typingTime);
        }

        yield return new WaitForSecondsRealtime(m_nexLineTime);
        //NextDialogueLine();
    }

    public void NextDialogueLine()
    {
        //SoundManager.instance.ReproduceSound(AudioClipsNames.Click, audioSource);

        if(m_TMPTxt_dialogueText.text == string.Empty)
            return; 

        if (m_TMPTxt_dialogueText.text == m_str_dialogueLines[m_lineIndex]) //si ense�a la linea completa
        {
            m_lineIndex++;
            if (m_lineIndex < m_str_dialogueLines.Length)
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
            if (m_lineIndex > m_str_dialogueLines.Length)
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
        m_TMPTxt_dialogueText.text = m_str_dialogueLines[m_lineIndex];
    }
}
