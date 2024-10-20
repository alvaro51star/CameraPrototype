using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;


public class WatchEnemy : MonoBehaviour
{
    [FormerlySerializedAs("enemy")] [SerializeField] private GameObject m_go_enemy;
    [FormerlySerializedAs("cameraTransform")] [SerializeField] private Transform m_tf_cameraTransform;
    [FormerlySerializedAs("maxDistance")]
    [Space]
    [Header("Angle of vision variables")]
    [SerializeField] private float m_maxDistance = 5f;
    [FormerlySerializedAs("maxAngleVision")] [SerializeField] private float m_maxAngleVision = 20f;

    [FormerlySerializedAs("maxAngleVisionJumpScare")]
    [Space]
    [Header("Jumpscare variables")]
    [SerializeField] private float m_maxAngleVisionJumpScare = 30f;
    [FormerlySerializedAs("timeForNewJumpScare")] [SerializeField] private float m_timeForNewJumpScare = 10f;

    [FormerlySerializedAs("feedbackVigneteEnemy")]
    [Space]
    [Header("Feel Variables")]
    [SerializeField] private MMFeedbacks m_mmf_feedbackVigneteEnemy;

    private bool isJumpScareOnCD = false;

    [FormerlySerializedAs("enemyCatchTp")] public Transform tf_enemyCatchTp;

    private void Update()
    {
        CanSeeEnemy();
        JumpScareStalker();
    }

    private void CanSeeEnemy()
    {
        if (m_go_enemy.layer != default)
        {
            return;
        }

        Vector3 rayDirection = m_go_enemy.GetComponent<StalkerBehaviour>().tf_pointToLook.position - m_tf_cameraTransform.position;
        if (Physics.Raycast(m_tf_cameraTransform.position, m_tf_cameraTransform.forward, out RaycastHit hit, m_maxDistance))
        {
            Debug.DrawRay(m_tf_cameraTransform.position, m_tf_cameraTransform.forward * m_maxDistance, Color.red);
            float angle = Vector3.Angle(m_tf_cameraTransform.forward, rayDirection);

            if (hit.transform.CompareTag("Enemy") && angle <= m_maxAngleVision)
            {
                Debug.DrawRay(m_tf_cameraTransform.position, m_tf_cameraTransform.forward * m_maxDistance, Color.green);
                hit.transform.GetComponent<StalkerBehaviour>().AddVision(Time.deltaTime);
            }
        }
    }

    private void JumpScareStalker()
    {
        Vector3 rayDirection = m_go_enemy.GetComponent<StalkerBehaviour>().tf_pointToLook.position - m_tf_cameraTransform.position;
        float distance = Vector3.Distance(transform.position, m_go_enemy.transform.position);
        if (Physics.Raycast(m_tf_cameraTransform.position, m_tf_cameraTransform.forward, out RaycastHit hit, m_maxDistance))
        {
            float angle = Vector3.Angle(rayDirection, transform.forward);
            if (hit.transform.CompareTag("Enemy") && angle <= m_maxAngleVisionJumpScare)
            {
                if (distance <= 5f && !isJumpScareOnCD)
                {
                    StartCoroutine(JumpScareCD());
                }
                else
                {
                    if (Random.Range(1, 6) < 3 && !isJumpScareOnCD)
                    {
                        StartCoroutine(JumpScareCD());
                    }
                }
            }
        }
    }

    private IEnumerator JumpScareCD()
    {
        isJumpScareOnCD = true;
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.jumpScare /*, this.transform.position */);
        yield return new WaitForSeconds(m_timeForNewJumpScare);
        isJumpScareOnCD = false;
    }

    public void ActivateFeedbacks()
    {
        m_mmf_feedbackVigneteEnemy?.PlayFeedbacks();
    }

    public void DesactivateFeedback()
    {
        m_mmf_feedbackVigneteEnemy?.StopFeedbacks();
    }
}
