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
        if (anomaliesCameraGO == null)
        {
            anomaliesCameraGO = GameObject.FindGameObjectWithTag("AnomalyCamera");
        }
        if (anomaliesCameraGO && !anomaliesCameraGO.activeSelf)
        {
            anomaliesCameraGO.SetActive(true);
        }
    }

    private void OnNotUsingCamera()
    {
        if (anomaliesCameraGO == null)
        {
            anomaliesCameraGO = GameObject.FindGameObjectWithTag("AnomalyCamera");
        }
        if (anomaliesCameraGO && !anomaliesCameraGO.activeSelf)
        {
            anomaliesCameraGO.SetActive(false);
        }
    }
}
