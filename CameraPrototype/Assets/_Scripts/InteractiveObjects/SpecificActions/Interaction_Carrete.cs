using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Carrete : InteractivePickUp
{
    //Variables
    [SerializeField] int m_carreteAñadido;
    public override void Action(GameObject player)
    {
        EventManager.OnAddRoll?.Invoke(m_carreteAñadido);
        UIManager.instance.ShowInput(false);
        ChangeActiveMode(gameObject, false);
    }
}
