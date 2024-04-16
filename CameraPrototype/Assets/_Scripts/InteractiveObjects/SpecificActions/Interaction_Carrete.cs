using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Carrete : InteractivePickUp
{
    //Variables
    [SerializeField] int m_carreteAņadido;
    public override void Action(GameObject player)
    {
        EventManager.OnAddRoll?.Invoke(m_carreteAņadido);
        UIManager.instance.ShowInput(false);
        ChangeActiveMode(gameObject, false);
    }
}
