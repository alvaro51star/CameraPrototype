using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AnomaliesData))]

public class AnomaliesLink : AnomalyBehaviour
{
     [SerializeField] private List<AnomaliesData> m_anomaliesDataL_anomaliesToReveal;//these anomalies don't need AnomalyBehaviour
     
    public override void PhotoAction()
    {
        RevealOtherAnomaly();
        base.PhotoAction();
    }
    private void RevealOtherAnomaly()
    {
        foreach (AnomaliesData anomaliesData in m_anomaliesDataL_anomaliesToReveal)
        {
            if(!anomaliesData)
                Debug.LogError(gameObject + " anomaliesToReveal is null");
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
