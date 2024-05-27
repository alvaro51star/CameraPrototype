using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_ObjetoMaldito : AffectsIndirectly
{
    [SerializeField] private TextForDialogue textForDialogue;//provisional
    [SerializeField] private GameObject m_finalTrigger;

    [SerializeField] private GameObject enemy;

    protected override void FirstAction()
    {
        Interaction_Door door = m_targetObject.GetComponent<Interaction_Door>();
        door?.SetlockDoor(m_affectsPositive);
        m_finalTrigger.SetActive(true);

        textForDialogue.StartDialogue();//provisional
        enemy.GetComponent<StalkerBehaviour>().EventPersecution();
    }

    // IEnumerator EnemyEvent()
    // {
    //     yield return new WaitForSeconds(0.1f);
    //     enemy.GetComponent<StalkerBehaviour>().EventPersecution();
    // }
}
