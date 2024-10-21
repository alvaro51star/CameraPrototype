using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Serialization;

public class AnomaliesData : MonoBehaviour
{
    public bool isRevealType;//hay 2 tipos de anomalia, las que revelas objetos y las que quitas objetos
    public Collider coll_anomalyColl;
    public bool isCullingVisible;//for culling group
    private void Start()
    {
        if (isRevealType)
        {
            transform.ChangeLayersRecursively(LayerMask.NameToLayer("Anomalies"));

            if (coll_anomalyColl)
                coll_anomalyColl.enabled = false;
            else
            {
                coll_anomalyColl = GetComponent<Collider>();
                if(!coll_anomalyColl)
                    Debug.LogError("Falta ponerle el anomalyCollider en AnomaliesData a " + gameObject);
                else
                {
                    coll_anomalyColl.enabled = false;
                }
                    
            }
        }
        else
        {
            transform.ChangeLayersRecursively(LayerMask.NameToLayer("Anomalies2"));
        }
    }
}
