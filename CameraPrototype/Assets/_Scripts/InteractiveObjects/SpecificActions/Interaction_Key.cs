using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Key : AffectsIndirectly
{
    protected override void FirstAction()
    {
        Interaction_Door door = m_go_targetObject.GetComponent<Interaction_Door>();
        door?.SetlockDoor(m_isAffectsPositive);
    }
}
