
using System.Collections;
using UnityEngine;

public class StalkerStalkState : StalkerBaseState
{
    private bool tpOnCooldown = false;

    public override void EnterState(StalkerStateManager stalker)
    {
        Debug.Log("Entrado en el StalkState");
        stalker.transform.position = StalkPointsManager.instance.activeStalkPoints[Random.Range(0, StalkPointsManager.instance.activeStalkPoints.Count)].transform.position;
    }

    public override void OnTriggerEnter(StalkerStateManager stalker)
    {

    }

    public override void UpdateState(StalkerStateManager stalker)
    {
        if (tpOnCooldown == false)
        {
            
        }
    }

    private IEnumerator TPtoNextPosition(StalkerStateManager stalker)
    {
        tpOnCooldown = true;
        stalker.transform.position = StalkPointsManager.instance.activeStalkPoints[Random.Range(0, StalkPointsManager.instance.activeStalkPoints.Count)].transform.position;
        yield return new WaitForSeconds(5);
        tpOnCooldown =false;
    }
}
