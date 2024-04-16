using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour, IInteractiveObject
{
    //Variables
    [SerializeField] Interactions interactionType;
    public void Interact()
    {
        if (interactionType == Interactions.Print)
        {
            print("Hola");
        }
    }
}
