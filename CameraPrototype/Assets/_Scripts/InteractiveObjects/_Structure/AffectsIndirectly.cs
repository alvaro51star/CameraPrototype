using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AffectsIndirectly : DoubleAction, I_ActivateDeactivate
{
    //Variables
    [SerializeField] protected bool m_isDissapears;
    [SerializeField] protected bool m_isAffectsPositive;
    [SerializeField] protected GameObject m_go_targetObject;
    
    public void IChangeActiveMode(GameObject gameObjectToDeactivate, bool mode)
    {
        gameObjectToDeactivate.SetActive(mode);
    }
    
    public override void Action(GameObject player)
    {
        if (m_isDissapears)
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
        EventManager.OnInteractiveObjectDisabled?.Invoke();
        IChangeActiveMode(gameObject, false);
    }


}
