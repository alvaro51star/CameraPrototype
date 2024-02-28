using UnityEngine;

public abstract class StalkerBaseState
{
    public bool isPlayerLooking;

    public abstract void EnterState(StalkerStateManager stalker);

    public abstract void UpdateState(StalkerStateManager stalker);

    public abstract void OnTriggerEnter(StalkerStateManager stalker);
}
