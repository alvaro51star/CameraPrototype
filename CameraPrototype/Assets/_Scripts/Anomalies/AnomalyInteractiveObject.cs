using UnityEngine;

public class AnomalyInteractiveObject : AnomalyBehaviour
{
    [SerializeField] private InteractiveObject interactiveObject;

    protected override void PhotoAction()
    {
        interactiveObject.enabled = true;
        
        base.PhotoAction();
    }
}
