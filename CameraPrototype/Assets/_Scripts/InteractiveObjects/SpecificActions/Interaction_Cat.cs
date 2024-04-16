using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Cat : InteractionScript
{
    //Variable
    public override void Action(GameObject player)
    {
        EventManager.OnCatPetted?.Invoke();
    }
}
