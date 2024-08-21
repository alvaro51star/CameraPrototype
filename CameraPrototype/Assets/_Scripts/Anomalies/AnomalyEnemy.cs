using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyEnemy : AnomalyBehaviour
{
    [SerializeField] private StalkerBehaviour stalkerBehaviour;
    protected override void PhotoAction()
    {
        stalkerBehaviour.ActivateCollision();
        stalkerBehaviour.enabled = true;
        EventManager.OnEnemyRevealed?.Invoke();
        
        base.PhotoAction();
    }

}
