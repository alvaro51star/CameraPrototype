using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealAnomalyManager : MonoBehaviour
{
    private bool canReveal = false;

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
    private void OnTriggerStay(Collider other)
    {        
        if(canReveal)
        {
            //necesario que las anomalias tengan Rigid Body y Collider
            if (other.gameObject.GetComponent<AnomaliesData>())
            {
                Vector3 directionRayCast = (other.GetComponent<Renderer>().bounds.center - transform.position).normalized;
                RaycastHit hit;
                float maxDistanceRaycast = Vector3.Distance(transform.position, other.transform.position);
                Debug.Log(other.name);
                Debug.DrawRay(transform.position, directionRayCast, Color.red);
                
                if(!Physics.Raycast(transform.position, directionRayCast, out hit, maxDistanceRaycast))
                {
                    Debug.Log("no hay nada delante de la anomalia");
                    other.gameObject.GetComponent<AnomaliesData>().AnomalyRevealed();
                    other.gameObject.GetComponent<AnomaliesData>().enabled = false;                                     
                }
                else if (Physics.Raycast(transform.position, directionRayCast, out hit, maxDistanceRaycast))
                {
                    if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Default"))
                    {
                        Debug.Log("anomalia parcialmente detectada");

                        other.gameObject.GetComponent<AnomaliesData>().AnomalyRevealed();
                        other.gameObject.GetComponent<AnomaliesData>().enabled = false;

                    }
                }
                
            }
        }
    }    

    private void OnTakingPhoto()
    {
        canReveal = true;
        GetComponent<BoxCollider>().enabled = true;
    }

    private void OnRemovePhoto()
    {
        canReveal = false;
        GetComponent<BoxCollider>().enabled = false;
    }

}
