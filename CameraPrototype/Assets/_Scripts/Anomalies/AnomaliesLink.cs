using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AnomaliesData))]

public class AnomaliesLink : AnomalyBehaviour
{
     [SerializeField] private List<AnomaliesData> anomaliesToReveal;//these anomalies don't need AnomalyBehaviour
    
    public override void PhotoAction()
    {
        RevealOtherAnomaly();
        base.PhotoAction();
    }
    private void RevealOtherAnomaly()
    {
        foreach (AnomaliesData anomaliesData in anomaliesToReveal)
        {
            if(!anomaliesData.isActiveAndEnabled)//if it is already revealed
                return;
            var anomalyBehaviour = anomaliesData.GetComponent<AnomalyBehaviour>();
            if (anomalyBehaviour)
            {
                anomalyBehaviour.PhotoAction();
            }
            else
            {
                base.RevealAnomaly(anomaliesData);
            }
            anomaliesData.enabled = false;
        }
        this.enabled = false;
    }
}
