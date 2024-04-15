using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        EventManager.CatPetted += OnCatPetted;
    }

    private void OnDisable()
    {
        EventManager.CatPetted -= OnCatPetted;
    }

    private void OnCatPetted()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
