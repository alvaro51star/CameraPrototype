using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyCamTrigger : MonoBehaviour
{
    private Collider m_collider;
    private StalkerBehaviour m_stalkerBehaviour;
    private void OnEnable()
    {
        EventManager.OnUsingCamera += OnUsingCamera;
        EventManager.OnTakingPhoto += OnTakingPhoto;
        EventManager.OnRemovePhoto += OnRemovePhoto;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= OnUsingCamera;
        EventManager.OnTakingPhoto -= OnTakingPhoto;
        EventManager.OnRemovePhoto -= OnRemovePhoto;
    }
    private void Start()
    {
        m_collider = GetComponent<Collider>();
    }
    private void OnUsingCamera()
    {
        m_collider.enabled = true;
    }

    private void OnTakingPhoto()
    {
        if(!m_stalkerBehaviour)
            return;
        m_stalkerBehaviour.StunEnemy();
    }

    private void OnRemovePhoto()
    {
        m_collider.enabled = false;        
        m_stalkerBehaviour = null;
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
        m_stalkerBehaviour = other.GetComponent<StalkerBehaviour>();
    }
}
