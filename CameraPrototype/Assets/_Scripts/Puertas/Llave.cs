using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : Puerta_Llave
{
    public bool unlockDoor = false;
    public bool isDoor = true;
    public MeshRenderer keyV;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip key;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            unlockDoor = true;
            isDoor = false;
            keyV.enabled = false;
            audioSource.clip = key;
            audioSource.Play();
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
