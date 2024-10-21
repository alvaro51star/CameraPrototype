using UnityEngine;

public class AnomalyEnemy : AnomalyBehaviour
{
    private StalkerBehaviour m_stalkerBehaviour;
    
    protected override void Start()
    {
        m_stalkerBehaviour = GetComponent<StalkerBehaviour>();
        if(!m_stalkerBehaviour)
            Debug.LogError("No hay StalkerBehaviour");
        m_stalkerBehaviour.enabled = false;
        
        base.Start();
    }
    public override void PhotoAction()
    {
        m_stalkerBehaviour.enabled = true;
        m_stalkerBehaviour.ActivateCollision();
        EventManager.OnEnemyRevealed?.Invoke();
        
        base.PhotoAction();
    }

}
