using UnityEngine;

public class Interaction_Cat : InteractionScript
{
    public override void Action(GameObject player)
    {
        //EventManager.OnCatPetted?.Invoke(); //ya no es necesario con los cambios de FMOD
    }
}
