using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float maxAngleVision = 15f;

    public Transform enemyCatchTp;

    private void Start()
    {

    }

    private void Update()
    {
        canSeeEnemy();
    }

    private void canSeeEnemy()
    {
        if (enemy.layer != default)
        {
            return;
        }

        Vector3 rayDirection = enemy.transform.position - transform.position;
        Debug.DrawRay(transform.position, rayDirection.normalized * maxDistance, Color.red);
        if (Physics.Raycast(transform.position, rayDirection.normalized, out RaycastHit hit, maxDistance))
        {
            float angle = Vector3.Angle(rayDirection, transform.forward);
            if (hit.transform.CompareTag("Enemy") && angle <= maxAngleVision)
            {
                hit.transform.GetComponent<StalkerBehaviour>().AddVision(Time.deltaTime);
            }
        }
    }
}
