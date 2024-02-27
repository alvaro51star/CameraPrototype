
using UnityEngine;

public class StalkerChaseState : StalkerBaseState
{


    public override void EnterState(StalkerStateManager stalker)
    {
        stalker.gameObject.transform.LookAt(stalker.player);
    }


    public override void UpdateState(StalkerStateManager stalker)
    {
        Debug.Log("Hola");
    }

    
    public override void OnTriggerEnter(StalkerStateManager stalker)
    {
        Debug.Log("Pillado");
    }
}
