using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSounds : MonoBehaviour
{
    [SerializeField] private AudioSource meowAudioSource;
    [SerializeField] private AudioSource growlAudioSource;

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
        /*
        if(!meowAudioSource.isPlaying)
        {
            // meowAudioSource.Play();
            Debug.Log("audio source play cat meow");
        }
        */
    }

    public void CatGrowl()
    {
        /*
        if (!growlAudioSource.isPlaying)
        {
            // growlAudioSource.Play();
        }
        */
    }

}
