using UnityEngine;

public class AnomalyInteractiveObject : AnomalyBehaviour
{
    private InteractiveObject m_interactiveObject;

    protected override void Start()
    {
        base.Start();
        m_interactiveObject = GetComponent<InteractiveObject>();
        m_interactiveObject.enabled = false;
    }

    public override void PhotoAction()
    {
        m_interactiveObject.enabled = true;
        
        base.PhotoAction();
    }
}
