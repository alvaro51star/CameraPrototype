using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;

    private EventInstance ambienceEventInstance;

    public static AudioManager Instance { get; private set; }

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

        eventInstances = new List<EventInstance>();
    }

    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.ambience);
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateEventInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    public void PlayOneShot (EventReference sound /*, Vector3 worldPos */)
    {
        RuntimeManager.PlayOneShot(sound /*, worldPos */);
    }

    public EventInstance CreateEventInstance (EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public void CleanUp()
    {
        // para y lanza las instancias creadas
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
