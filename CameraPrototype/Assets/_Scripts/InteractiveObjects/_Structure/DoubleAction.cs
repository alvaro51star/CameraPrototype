using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DoubleAction : InteractionScript
{
    //Variables
    [SerializeField] protected string m_str_firstMessage;
    [SerializeField] protected string m_str_secondMessage;
    protected bool m_isFirstAction = true;
    
    protected virtual void Start()
    {
        m_str_interactText = m_str_firstMessage;
    }

    //Custom
    public override void Action(GameObject player)
    {
        if (m_isFirstAction)
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
        m_isFirstAction = false;
        if (m_str_secondMessage != "")
        {
            m_str_interactText = m_str_secondMessage;
        }
    }

    protected virtual void SecondActon()
    {
        m_isFirstAction = true;
        m_str_interactText = m_str_firstMessage;
    }
}
