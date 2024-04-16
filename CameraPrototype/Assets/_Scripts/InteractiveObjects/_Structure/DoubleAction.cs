using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAction : InteractionScript
{
    //Variables
    protected bool m_firstAction = true;
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
        print("FirstAction");
    }

    protected virtual void SecondActon()
    {
        m_firstAction = true;
        print("SecondAction");
    }
}
