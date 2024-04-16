using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Key : InteractivePickUp
{
    //Variables
    [SerializeField] Interaction_Door m_door;
    public override void Action()
    {
        m_door.UnlockDoor();
        print("abro puerta");
        base.Action();
    }
}
