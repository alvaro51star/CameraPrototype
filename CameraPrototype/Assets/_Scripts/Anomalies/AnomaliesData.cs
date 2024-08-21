using MoreMountains.Tools;
using UnityEngine;

public class AnomaliesData : MonoBehaviour
{
    public bool revealType;//hay 2 tipos de anomalia, las que revelas objetos y las que quitas objetos

    private void Start() //innecesario (esta ya puesto en editor), solo por si acaso
    {
        if (revealType)
        {
            //gameObject.layer = LayerMask.NameToLayer("Anomalies");
            transform.ChangeLayersRecursively(LayerMask.NameToLayer("Anomalies"));
            //Para cambiar tb todos los hijos
             /*for (int i = 0; i < transform.childCount; i++)
             {
                 transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Anomalies");
             }*/

            if (GetComponent<StalkerBehaviour>())
            {
                GetComponent<StalkerBehaviour>().enabled = false;
            }
            else if(GetComponent<Llave>())
            {
                GetComponent<Llave>().enabled = false;
            }
        }
        else
        {
            //gameObject.layer = LayerMask.NameToLayer("Anomalies2");
            transform.ChangeLayersRecursively(LayerMask.NameToLayer("Anomalies2"));
             /*for (int i = 0; i < transform.childCount; i++)
             {
                 transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Anomalies2");
             }*/
        }
    }
}