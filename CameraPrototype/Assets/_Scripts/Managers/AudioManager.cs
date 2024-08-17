using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


// [RequireComponent(typeof(AudioSource), typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }

    // [SerializeField] private AudioSource audioSourceSFX;
    // [SerializeField] private AudioSource audioSourceMusic;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayOneShot (EventReference sound /*, Vector3 worldPos */)
    {
        RuntimeManager.PlayOneShot(sound /*, worldPos */);
    }

    public void ReproduceSound(AudioClip audioClip)
    {
        //audioSourceSFX.PlayOneShot(audioClip);
        Debug.Log("play one shot");
    }

    public EventInstance CreateEventInstance (EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
