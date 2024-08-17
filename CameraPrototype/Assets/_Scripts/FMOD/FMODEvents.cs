using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }

    // AudioManager.Instance.PlayOneShot(FMODEvents.instance.xSound /*, this.transform.position */);

    [field: Header("Camera SFX")]
    [field: SerializeField] public EventReference anomalyDetected { get; private set; }
    [field: SerializeField] public EventReference cameraClickClip { get; private set; }
    [field: SerializeField] public EventReference noPhotosClip { get; private set; }

    [field: Header("Enemy SFX")]
    [field: SerializeField] public EventReference stalkerBreathingSound { get; private set; }

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
