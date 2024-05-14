using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAction : InteractionScript
{
    //Variables
    protected bool m_firstAction = true;
    [SerializeField] protected string m_firstMessage;
    [SerializeField] protected string m_secondMessage;
    protected virtual void Start()
    {
        m_interactText = m_firstMessage;
    }

    public override void Action(GameObject player)
    {
        if (m_firstAction)
        {
            FirstAction();
        }
        else
        {
            SecondActon();
        }

    }

    protected virtual void FirstAction()
    {
        m_firstAction = false;
        if (m_secondMessage != "")
        {
            m_interactText = m_secondMessage;
        }
    }

    protected virtual void SecondActon()
    {
        m_firstAction = true;
        m_interactText = m_firstMessage;
    }
}
