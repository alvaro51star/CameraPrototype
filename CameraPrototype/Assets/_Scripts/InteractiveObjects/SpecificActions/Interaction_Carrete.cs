using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Carrete : AffectsIndirectly
{
    //Variables
    [SerializeField] int m_carreteAņadido;

    protected override void FirstAction()
    {
        EventManager.OnAddRoll?.Invoke(m_carreteAņadido);
        UIManager.instance.ShowInput(false);
        ChangeActiveMode(gameObject, false);
    }
}
