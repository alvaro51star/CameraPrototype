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
    private void OnTriggerStay(Collider other)
    {        
        if(canReveal)
        {
            //necesario que las anomalias tengan Rigid Body, Collider y layer "Anomalies"
            if (other.gameObject.GetComponent<AnomaliesData>())
            {
                other.gameObject.GetComponent<AnomaliesData>().AnomalyRevealed();
                other.gameObject.GetComponent<AnomaliesData>().enabled = false;
            }
        }
    }    

    private void OnTakingPhoto()
    {
        canReveal = true;
    }

    private void OnRemovePhoto()
    {
        canReveal = false;
    }
    
}
