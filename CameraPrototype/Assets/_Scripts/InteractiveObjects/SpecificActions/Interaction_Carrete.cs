using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Carrete : InteractivePickUp
{
    //Variables
    [SerializeField] int m_carreteA�adido;
    public override void Action(GameObject player)
    {
        EventManager.OnAddRoll?.Invoke(m_carreteA�adido);
        UIManager.instance.ShowInput(false);
        ChangeActiveMode(gameObject, false);
    }
}
