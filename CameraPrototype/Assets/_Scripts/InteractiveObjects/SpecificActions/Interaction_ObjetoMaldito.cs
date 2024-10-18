using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Interaction_ObjetoMaldito : AffectsIndirectly
{
    [SerializeField] private TextForDialogue m_textForDialogue;//provisional
    [SerializeField] private GameObject m_GO_finalTrigger, m_GO_enemy;

    private void OnEnable()
    {
        EventManager.OnStopReading += TimeScaleNormal;
    }

    private void OnDisable()
    {
        EventManager.OnStopReading -= TimeScaleNormal;
    }

    //Custom
    protected override void FirstAction()
    {
        Interaction_Door door = m_GO_targetObject.GetComponent<Interaction_Door>();
        door?.SetlockDoor(m_isAffectsPositive);
        m_GO_finalTrigger!.SetActive(true);

        m_textForDialogue.StartDialogue();//provisional
        //Time.timeScale = 0;
        m_GO_enemy.GetComponent<StalkerBehaviour>().EventPersecution();
        //StartCoroutine(EnemyEvent());
    }

    IEnumerator EnemyEvent()
    {
        yield return new WaitForSeconds(0.1f);
        m_GO_enemy.GetComponent<StalkerBehaviour>().EventPersecution();
    }
    
    public void TimeScaleNormal()
    {
        Time.timeScale = 1;
    }

    public void TimeScaleStopped()
    {
        Time.timeScale = 0;
    }
}
