using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private PhotoCapture m_photoCapture;
    [SerializeField] private GameObject CameraUI;
    private void OnEnable()
    {
        EventManager.OnUsingCamera += OnUsingCamera;
        EventManager.OnNotUsingCamera += OnNotUsingCamera;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= OnUsingCamera;
        EventManager.OnNotUsingCamera -= OnNotUsingCamera;
    }

    private void OnUsingCamera()
    {
        m_photoCapture.SetHasCameraEquiped(true);
        CameraUI.SetActive(true);
    }

    private void OnNotUsingCamera()
    {
        m_photoCapture.SetHasCameraEquiped(false);
        CameraUI.SetActive(false);
    }
}
