using MoreMountains.Tools;
using UnityEngine;

public class AnomaliesData : MonoBehaviour
{
    public bool revealType;//hay 2 tipos de anomalia, las que revelas objetos y las que quitas objetos
    public Collider anomalyCollider;
    public bool cullingIsVisible;//for culling group
    private void Start()
    {
        if (revealType)
        {
            transform.ChangeLayersRecursively(LayerMask.NameToLayer("Anomalies"));

            if (anomalyCollider)
                anomalyCollider.enabled = false;
            else
            {
                anomalyCollider = GetComponent<Collider>();
                if(!anomalyCollider)
                    Debug.LogError("Falta ponerle el anomalyCollider en AnomaliesData a " + gameObject);
                else
                {
                    anomalyCollider.enabled = false;
                }
                    
            }
        }
        else
        {
            transform.ChangeLayersRecursively(LayerMask.NameToLayer("Anomalies2"));
        }
    }
}
