using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Interaction_ObjetoMaldito : AffectsIndirectly
{
    [SerializeField] private GameObject m_go_enemy;
    [SerializeField] private GameObject m_go_finalTrigger;
    [SerializeField] private TextForDialogue m_textForDialogue;//provisional

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
        Interaction_Door door = m_go_targetObject.GetComponent<Interaction_Door>();
        door?.SetlockDoor(m_isAffectsPositive);
        m_go_finalTrigger!.SetActive(true);

        m_textForDialogue.StartDialogue();//provisional
        //Time.timeScale = 0;
        m_go_enemy.GetComponent<StalkerBehaviour>().EventPersecution();
        //StartCoroutine(EnemyEvent());
    }

    IEnumerator EnemyEvent()
    {
        yield return new WaitForSeconds(0.1f);
        m_go_enemy.GetComponent<StalkerBehaviour>().EventPersecution();
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
