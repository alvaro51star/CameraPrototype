using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyRadar : MonoBehaviour
{
    [SerializeField] private GameObject anomalyRadarUI;
    [SerializeField] private AudioSource audioSource;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<AnomaliesData>() && other.gameObject.GetComponent<AnomaliesData>().enabled)
        {
            audioSource.Play();
            anomalyRadarUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<AnomaliesData>() && other.gameObject.GetComponent<AnomaliesData>().enabled)
        {
            anomalyRadarUI.SetActive(false);
        }
    }

    private void OnUsingCamera()
    {
        this.gameObject.GetComponent<Collider>().enabled = true;
    }
    private void OnNotUsingCamera()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
    }
}
