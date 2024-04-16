using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractivePickUp : InteractionScript, I_ActivateDeactivate
{
    //Variables
    public override void Action(GameObject player)
    {
        UIManager.instance.ShowInput(false);
        ChangeActiveMode(gameObject, false);
    }
    public void ChangeActiveMode(GameObject gameObjectToDeactivate, bool mode)
    {
        gameObjectToDeactivate.SetActive(mode);
    }
}
