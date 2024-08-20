using UnityEngine;

public class AnomalyInteractiveObject : AnomalyBehaviour
{
    [SerializeField] private InteractiveObject interactiveObject;
    [SerializeField] private Interaction_Key interactionKey;
    protected override void PhotoAction()
    {
        interactiveObject.enabled = true;
        interactionKey.enabled = true;
        base.PhotoAction();
    }
}
