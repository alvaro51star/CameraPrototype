using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealAnomalyManager : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    private void OnEnable()
    {
        EventManager.OnTakingPhoto += OnTakingPhoto;
        EventManager.OnRemovePhoto += OnRemovePhoto;
    }
    private void OnDisable()
    {
        EventManager.OnTakingPhoto -= OnTakingPhoto;
        EventManager.OnRemovePhoto -= OnRemovePhoto;
    }
    private void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //necesario que las anomalias tengan Rigid Body y Collider
        if (other.gameObject.GetComponent<AnomaliesData>() && other.gameObject.GetComponent<AnomaliesData>().enabled)
        {
            Debug.Log(other.name + "has entered anomalies trigger");
            Vector3 directionRayCast = (other.GetComponent<Renderer>().bounds.center - transform.position).normalized;
            RaycastHit hit;
            float maxDistanceRaycast = Vector3.Distance(transform.position, other.GetComponent<Renderer>().bounds.center);
            Debug.DrawRay(transform.position, directionRayCast * maxDistanceRaycast, Color.red);

            if (Physics.Raycast(transform.position, directionRayCast, out hit, maxDistanceRaycast))
            {
                if(hit.collider.gameObject == other.gameObject)
                {
                    if (hit.collider.gameObject.GetComponent<AnomaliesData>())
                    {
                        hit.collider.gameObject.GetComponent<AnomaliesData>().AnomalyRevealed();
                        hit.collider.gameObject.GetComponent<AnomaliesData>().enabled = false;
                        Debug.Log(other.name + "is revealed");
                    }
                }                
            }
        }

        if (other.gameObject.CompareTag("Enemy") && !other.gameObject.GetComponent<AnomaliesData>().enabled)
        {
            other.GetComponent<StalkerBehaviour>().StunEnemy();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            boxCollider.enabled = true;
        }
    }

    private void OnTakingPhoto()
    {
        boxCollider.enabled = true;
        Debug.Log("Is taking photo");
    }

    private void OnRemovePhoto()
    {
        boxCollider.enabled = false;
        Debug.Log("Is removing photo");
    }

}
