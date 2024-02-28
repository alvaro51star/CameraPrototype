
using System.Collections;
using UnityEngine;

public class StalkerStalkState : StalkerBaseState
{
    private bool tpOnCooldown = false;
    private float maxTime = 5f;
    private float currentTime = 0f;

    public override void EnterState(StalkerStateManager stalker)
    {
        Debug.Log("Entrado en el StalkState");
        //stalker.transform.position = StalkPointsManager.instance.activeStalkPoints[Random.Range(0, StalkPointsManager.instance.activeStalkPoints.Count)].transform.position;
    }

    public override void OnTriggerEnter(StalkerStateManager stalker)
    {

    }

    public override void UpdateState(StalkerStateManager stalker)
    {
        currentTime += Time.deltaTime;

        if (currentTime <= 0 && !isPlayerLooking)
        {
            TPtoNextPosition(stalker);
            currentTime = maxTime;
        }
    }

    private void TPtoNextPosition(StalkerStateManager stalker)
    {
        if (StalkPointsManager.instance.activeStalkPoints.Count <= 0)
        {
            return;
        }
        stalker.transform.position = StalkPointsManager.instance.activeStalkPoints[Random.Range(0, StalkPointsManager.instance.activeStalkPoints.Count)].transform.position;
    }
}
