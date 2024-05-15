using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyCameraManager : MonoBehaviour
{
    [SerializeField] private GameObject anomaliesCameraGO;
    private void OnEnable()
    {
        EventManager.OnUsingCamera += OnUsingCamera;
        EventManager.OnNotUsingCamera += OnNotUsingCamera;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera += OnUsingCamera;
        EventManager.OnNotUsingCamera += OnNotUsingCamera;
    }

    private void Start()
    {
        anomaliesCameraGO.SetActive(false);
    }

    private void OnUsingCamera()
    {
        if (!anomaliesCameraGO)
            anomaliesCameraGO = GameObject.FindGameObjectWithTag("AnomalyCamera");
        anomaliesCameraGO.SetActive(true);
    }

    private void OnNotUsingCamera()
    {
        anomaliesCameraGO.SetActive(false);
    }
}
