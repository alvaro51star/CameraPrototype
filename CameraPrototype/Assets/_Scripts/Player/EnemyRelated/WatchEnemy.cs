using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float maxAngleVision = 15f;
    [SerializeField] private float maxAngleVisionJumpScare = 30f;
    [SerializeField] private float timeForNewJumpScare = 10f;

    [SerializeField] private AudioClip jumpScareSoundEffect;

    private bool jumpScareOnCD = false;

    public Transform enemyCatchTp;

    private void Update()
    {
        CanSeeEnemy();
        JumpScareStalker();
    }

    private void CanSeeEnemy()
    {
        if (enemy.layer != default)
        {
            return;
        }

        Vector3 rayDirection = enemy.transform.position - transform.position;
        Debug.DrawRay(transform.position, rayDirection.normalized * maxDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance))
        {
            float angle = Vector3.Angle(rayDirection, transform.forward);
            if (hit.transform.CompareTag("Enemy") && angle <= maxAngleVision && enemy.GetComponent<StalkerBehaviour>().objectMesh.isVisible)
            {
                hit.transform.GetComponent<StalkerBehaviour>().AddVision(Time.deltaTime);
            }
        }
    }

    private void JumpScareStalker()
    {
        Vector3 rayDirection = enemy.transform.position - transform.position;
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance) && enemy.GetComponent<StalkerBehaviour>().objectMesh.isVisible)
        {
            float angle = Vector3.Angle(rayDirection, transform.forward);
            if (hit.transform.CompareTag("Enemy") && angle <= maxAngleVisionJumpScare)
            {
                if (distance <= 5f && !jumpScareOnCD)
                {
                    StartCoroutine(JumpScareCD());
                }
                else
                {
                    if (Random.Range(1, 6) < 3 && !jumpScareOnCD)
                    {
                        StartCoroutine(JumpScareCD());
                    }
                }
            }
        }
    }

    private IEnumerator JumpScareCD()
    {
        jumpScareOnCD = true;
        AudioManager.Instance.ReproduceSound(jumpScareSoundEffect);
        yield return new WaitForSeconds(timeForNewJumpScare);
        jumpScareOnCD = false;
    }
}
