using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_ObjetoMaldito : AffectsIndirectly
{
    [SerializeField] private TextForDialogue textForDialogue;//provisional
    [SerializeField] private GameObject m_finalTrigger;
    protected override void FirstAction()
    {
        Interaction_Door door = m_targetObject.GetComponent<Interaction_Door>();
        door?.SetlockDoor(m_affectsPositive);
        m_finalTrigger.SetActive(true);
        textForDialogue.StartDialogue();//provisional
    }
}
