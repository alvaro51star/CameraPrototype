using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectsIndirectly : DoubleAction, I_ActivateDeactivate
{
    //Variables
    [SerializeField] protected GameObject m_targetObject;
    [SerializeField] protected bool m_dissapears;
    public override void Action(GameObject player)
    {
        if (m_dissapears)
        {
            JustOnce();
        }
        else
        {
            base.Action(player);
        }
    }

    protected virtual void JustOnce()
    {
        FirstAction();
        Disappear();
    }

    protected virtual void Disappear()
    {
        ChangeActiveMode(gameObject, false);
    }


    public void ChangeActiveMode(GameObject gameObjectToDeactivate, bool mode)
    {
        gameObjectToDeactivate.SetActive(mode);
    }
}
