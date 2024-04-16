using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void OnEnable()
    {
        EventManager.OnCatPetted += OnCatPetted;
    }

    private void OnDisable()
    {
        EventManager.OnCatPetted -= OnCatPetted;
    }

    private void OnCatPetted()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
