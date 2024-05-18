using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFPSManager : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }
}
