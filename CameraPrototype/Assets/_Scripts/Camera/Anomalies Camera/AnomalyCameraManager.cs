using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyCameraManager : MonoBehaviour
{
    [SerializeField] private GameObject AnomaliesCameraGO;
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

    private void OnUsingCamera()
    {
        AnomaliesCameraGO.SetActive(true);
    }

    private void OnNotUsingCamera()
    {
        AnomaliesCameraGO.SetActive(false);
    }
}
