using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : Puerta_Llave
{
    public bool unlockDoor = false;
    public bool isDoor = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            unlockDoor = true;
            isDoor = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDoor = true;
        }
    }
}
