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
        StartCoroutine(WaitEndFrameToStun());
    }

    private void OnRemovePhoto()
    {
        m_collider.enabled = false;
        m_stalkerBehaviour = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        AnomalyBehaviour anomalyBehaviour = other.GetComponent<AnomalyBehaviour>();
        if (!anomalyBehaviour)
            return;
        anomalyBehaviour.isInPlayersTrigger = true;

        StalkerBehaviour stalkerBehaviour = other.GetComponent<StalkerBehaviour>();
        if (!stalkerBehaviour)
            return;
        m_stalkerBehaviour = stalkerBehaviour;
    }

    private IEnumerator WaitEndFrameToStun()//needs to be done after enemy has a state
    {
        yield return new WaitForEndOfFrame();

        if (!m_stalkerBehaviour)
            yield break;
        if (!m_stalkerBehaviour.isActiveAndEnabled)
            yield break;
        if (m_stalkerBehaviour.IsAttackingPlayer())
            yield break;

        m_stalkerBehaviour.StunEnemy();
    }
}
