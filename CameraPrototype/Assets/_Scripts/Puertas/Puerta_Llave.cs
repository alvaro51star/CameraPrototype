using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta_Llave : MonoBehaviour
{
    Animator ANIM;
    public Llave llave;
    private bool openDoor;
    //[SerializeField] string nombreYApellido;

    private void Start()
    {
        ANIM = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (llave.unlockDoor == true)
            {
                Debug.Log("Funciona LLAVE Y PUERTA");
                openDoor = true;
                ANIM.SetBool("Open", openDoor);
            }
        }
    }
}
