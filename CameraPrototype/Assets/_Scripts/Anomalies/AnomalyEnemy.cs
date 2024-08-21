using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyEnemy : AnomalyBehaviour
{
    private StalkerBehaviour _stalkerBehaviour;
    
    protected override void Start()
    {
        _stalkerBehaviour = GetComponent<StalkerBehaviour>();
        if(!_stalkerBehaviour)
            Debug.LogError("No hay StalkerBehaviour");
        _stalkerBehaviour.enabled = false;
        
        base.Start();
    }
    public override void PhotoAction()
    {
        _stalkerBehaviour.enabled = true;
        Debug.Log(_stalkerBehaviour);
        _stalkerBehaviour.ActivateCollision();
        EventManager.OnEnemyRevealed?.Invoke();
        
        base.PhotoAction();
    }

}
