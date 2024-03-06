using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    Animator ANIM;
    private bool openDoor = false;
    private void Start()
    {
        ANIM = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openDoor = true;
            ANIM.SetBool("Open", openDoor);
        }
    }

}
