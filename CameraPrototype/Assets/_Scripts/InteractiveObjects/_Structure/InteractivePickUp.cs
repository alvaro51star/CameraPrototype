using UnityEngine;

public class InteractivePickUp : AffectsIndirectly
{
    public override void Action(GameObject player)
    {
        IChangeActiveMode(gameObject, false);
    }
}
