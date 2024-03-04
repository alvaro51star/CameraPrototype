using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : Puerta_Llave
{
    public bool unlockDoor = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                unlockDoor = true;
                Debug.Log("funciona LLAVE");
            }
        }
    }
}
