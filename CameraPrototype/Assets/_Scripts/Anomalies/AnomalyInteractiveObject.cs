using UnityEngine;

public class AnomalyInteractiveObject : AnomalyBehaviour
{
    private InteractiveObject _interactiveObject;

    protected override void Start()
    {
        base.Start();
        _interactiveObject = GetComponent<InteractiveObject>();
        _interactiveObject.enabled = false;
    }

    public override void PhotoAction()
    {
        _interactiveObject.enabled = true;
        
        base.PhotoAction();
    }
}
