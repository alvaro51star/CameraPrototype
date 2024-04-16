using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject CameraManagerGO;
    [SerializeField] private GameObject CameraUI;
    private void OnEnable()
    {
        EventManager.UsingCamera += OnUsingCamera;
        EventManager.NotUsingCamera += OnNotUsingCamera;
    }

    private void OnDisable()
    {
        EventManager.UsingCamera -= OnUsingCamera;
        EventManager.NotUsingCamera -= OnNotUsingCamera;
    }

    private void OnUsingCamera()
    {
        CameraManagerGO.SetActive(true);
        CameraUI.SetActive(true);
    }

    private void OnNotUsingCamera()
    {
        CameraManagerGO.SetActive(false);
        CameraUI.SetActive(false);
    }
}
