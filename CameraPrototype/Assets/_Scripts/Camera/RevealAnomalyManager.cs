using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealAnomalyManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.TakingPhoto += OnTakingPhoto;
        EventManager.RemovePhoto += OnRemovePhoto;
    }
    private void OnDisable()
    {
        EventManager.TakingPhoto -= OnTakingPhoto;
        EventManager.RemovePhoto -= OnRemovePhoto;
    }
    private void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //necesario que las anomalias tengan Rigid Body y Collider
        if (other.gameObject.GetComponent<AnomaliesData>())
        {
            Vector3 directionRayCast = (other.GetComponent<Renderer>().bounds.center - transform.position).normalized;
            RaycastHit hit;
            float maxDistanceRaycast = Vector3.Distance(transform.position, other.GetComponent<Renderer>().bounds.center);
            //Debug.DrawRay(transform.position, directionRayCast * maxDistanceRaycast, Color.red);

            if (Physics.Raycast(transform.position, directionRayCast, out hit, maxDistanceRaycast))
            {
                if(hit.collider.gameObject == other.gameObject)
                {
                    if (hit.collider.gameObject.GetComponent<AnomaliesData>())
                    {
                        hit.collider.gameObject.GetComponent<AnomaliesData>().AnomalyRevealed();
                        hit.collider.gameObject.GetComponent<AnomaliesData>().enabled = false;
                    }
                }                
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            //other.GetComponent<StalkerBehaviour>().StunEnemy();
        }
    }    

    private void OnTakingPhoto()
    {
        GetComponent<BoxCollider>().enabled = true;
    }

    private void OnRemovePhoto()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

}
