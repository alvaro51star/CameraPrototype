using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealAnomalyManager : MonoBehaviour
{
    private bool canReveal = false;

    private void OnEnable()
    {
        EventManager.TakingPhoto += OnTakingPhoto;
    }
    private void OnDisable()
    {
        EventManager.TakingPhoto -= OnTakingPhoto;
    }
    private void OnTriggerStay(Collider other)
    {        
        if(canReveal)
        {
            //necesario que las anomalias tengan Rigid Body, Collider y layer "Anomalies"
            if (other.gameObject.layer == LayerMask.NameToLayer("Anomalies")) 
            {
                RevealAnomalies(other.gameObject);
            }
        }
    }    

    private void OnTakingPhoto()
    {
        canReveal = true;
    }

    private void RevealAnomalies(GameObject otherGO)
    {
        otherGO.layer = LayerMask.NameToLayer("Default");
        canReveal = false;
    }
}
