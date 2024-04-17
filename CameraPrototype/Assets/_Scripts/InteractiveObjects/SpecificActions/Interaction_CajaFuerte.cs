using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_CajaFuerte : AffectsIndirectly
{
    //Variables 
    [SerializeField] private string m_correctCode;
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
                ChangeActiveMode(m_targetObject, true);
                m_hasOpened = true;
            }
        }
    }

    public void AddActualCode(GameObject button)
    {
        m_actualCode += button.name;
        UIManager.instance.ChangeCodeDisplay(m_actualCode);
        UIManager.instance.ShowLight(false, true);
    }

    public void DeleteActualCode()
    {
        m_actualCode = "";
        UIManager.instance.ChangeCodeDisplay(m_actualCode);
    }

    public void CheckCode()
    {
        if (m_actualCode == m_correctCode)
        {
            m_isOpen = true;
            UIManager.instance.ShowLight(true, false);
        }
        else
        {
            UIManager.instance.ShowLight(true, true);
        }
    }
}
