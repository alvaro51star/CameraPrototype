using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VCAsController : MonoBehaviour
{
    private FMOD.Studio.VCA VCAController;
    private Slider slider;
    public string VCAName;

    void Start()
    {
        VCAController = FMODUnity.RuntimeManager.GetVCA("vca:/" + VCAName);
        slider = GetComponent<Slider>();
    }

    public void SetVolume(float volParameter)
    {
        VCAController.setVolume(volParameter);
    }
}
