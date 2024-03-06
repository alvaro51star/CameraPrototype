using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : Puerta_Llave
{
    public bool unlockDoor = false;
    public bool isDoor = true;
    public MeshRenderer keyV;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            unlockDoor = true;
            isDoor = false;
            keyV.enabled = false;
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
