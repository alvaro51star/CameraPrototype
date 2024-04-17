using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_CajaFuerte : AffectsIndirectly
{
    //Variables 
    [SerializeField] private string m_correctCode;
    [SerializeField] private AudioClip[] m_keySounds;
    [SerializeField] private AudioClip m_wrongSound;
    [SerializeField] private AudioClip m_rightSound;
    [SerializeField] private AudioClip m_safeUnlockSound;
    [SerializeField] private AudioClip m_safeOpen;
    private bool m_isOpen = false;
    private string m_actualCode = "";
    private bool m_hasOpened;

    protected override void FirstAction()
    {
        if (!m_hasOpened)
        {
            if (!m_isOpen)
            {
                UIManager.instance.ShowSafe(true);
                EventManager.OnIsReading?.Invoke();
            }
            else
            {
                AudioManager.Instance.ReproduceSound(m_safeOpen);
                ChangeActiveMode(m_targetObject, true);
                m_hasOpened = true;
            }
        }
    }

    public void AddActualCode(GameObject button)
    {
        ReproduceKeySound();
        m_actualCode += button.name;
        UIManager.instance.ChangeCodeDisplay(m_actualCode);
        UIManager.instance.ShowLight(false, true);
    }

    public void ReproduceKeySound()
    {
        AudioClip clip = m_keySounds[Random.Range(0, m_keySounds.Length)];
        AudioManager.Instance.ReproduceSound(clip);
    }

    public void DeleteActualCode()
    {
        ReproduceKeySound();
        m_actualCode = "";
        UIManager.instance.ChangeCodeDisplay(m_actualCode);
    }

    public void CheckCode()
    {
        ReproduceKeySound();
        if (m_actualCode == m_correctCode)
        {
            m_isOpen = true;
            UIManager.instance.ShowLight(true, false);
            AudioManager.Instance.ReproduceSound(m_rightSound);
            AudioManager.Instance.ReproduceSound(m_safeUnlockSound);
        }
        else
        {
            UIManager.instance.ShowLight(true, true);
            AudioManager.Instance.ReproduceSound(m_wrongSound);
        }
    }
}
