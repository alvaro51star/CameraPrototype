using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomaliesLink : MonoBehaviour
{
    [SerializeField] private GameObject anomalyToReveal;
    public void RevealOtherAnomaly()
    {
        if(!anomalyToReveal.GetComponent<AnomalyBehaviour>().isActiveAndEnabled)
            return;
        anomalyToReveal.GetComponent<AnomalyBehaviour>().RevealAnomaly();
        this.enabled = false;
    }
}
