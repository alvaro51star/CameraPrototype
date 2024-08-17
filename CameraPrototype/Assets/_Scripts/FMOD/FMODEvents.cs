using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }

    // AudioManager.Instance.PlayOneShot(FMODEvents.instance.xSound /*, this.transform.position */);

    [field: Header("Ambience&Music")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Camera SFX")]
    [field: SerializeField] public EventReference anomalyDetected { get; private set; }
    [field: SerializeField] public EventReference cameraClickClip { get; private set; }
    [field: SerializeField] public EventReference noPhotosClip { get; private set; }

    [field: Header("InteractiveObjects SFX")]
    [field: SerializeField] public EventReference openDoor { get; private set; }
    [field: SerializeField] public EventReference closeDoor { get; private set; }
    [field: SerializeField] public EventReference doorLocked { get; private set; }
    [field: SerializeField] public EventReference key { get; private set; }
    [field: SerializeField] public EventReference paper { get; private set; }
    [field: SerializeField] public EventReference safeboxButtons { get; private set; }
    [field: SerializeField] public EventReference sandBox { get; private set; }
    [field: SerializeField] public EventReference rightCode { get; private set; }
    [field: SerializeField] public EventReference wrongCode { get; private set; }
    [field: SerializeField] public EventReference unlockSafe { get; private set; }
    [field: SerializeField] public EventReference openSafe { get; private set; }


    [field: Header("Enemy SFX")]
    [field: SerializeField] public EventReference stalkerBreathingSound { get; private set; }
    [field: SerializeField] public EventReference stalkerGrowling { get; private set; }
    [field: SerializeField] public EventReference jumpScare { get; private set; }
    [field: SerializeField] public EventReference caught { get; private set; }


    [field: Header("Cat SFX")]
    [field: SerializeField] public EventReference meow { get; private set; }
    [field: SerializeField] public EventReference growl { get; private set; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
