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
        _stalkerBehaviour.ActivateCollision();
        EventManager.OnEnemyRevealed?.Invoke();
        
        base.PhotoAction();
    }

}
