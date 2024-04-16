using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Variables
    private void OnTriggerEnter(Collider other)
    {
        InteractiveObject interactiveObject = other.GetComponent<InteractiveObject>();
        if (interactiveObject != null)
        {
            Debug.Log("HI");
            interactiveObject.Interact();
        }
    }
}
