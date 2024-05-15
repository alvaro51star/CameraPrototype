using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyCamTrigger : MonoBehaviour
{
    private Collider m_collider;
    private void OnEnable()
    {
        EventManager.OnUsingCamera += OnTakingPhoto;
        EventManager.OnRemovePhoto += OnRemovePhoto;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= OnTakingPhoto;
        EventManager.OnRemovePhoto -= OnRemovePhoto;
    }
    private void Start()
    {
        m_collider = GetComponent<Collider>();
    }
    private void OnTakingPhoto()
    {
        m_collider.enabled = true;
    }

    private void OnRemovePhoto()
    {
        m_collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<AnomalyBehaviour>())
            return;
        if (other.GetComponent<AnomalyBehaviour>().isActiveAndEnabled)
        {
            other.GetComponent<AnomalyBehaviour>().isInPlayersTrigger = true;
        }
        if(!other.GetComponent<StalkerBehaviour>())
            return;
        if(!other.GetComponent<StalkerBehaviour>().isActiveAndEnabled)
            return;
        other.GetComponent<StalkerBehaviour>().StunEnemy();
    }
}
