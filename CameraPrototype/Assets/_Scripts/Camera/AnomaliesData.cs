using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnomaliesData : MonoBehaviour
{
    public bool revealType;//hay 2 tipos de anomalia, las que revelas objetos y las que quitas objetos
    
    private void Start() //innecesario (esta ya puesto en editor), solo por si acaso
    {
        if(revealType)
        {
            gameObject.layer = LayerMask.NameToLayer("Anomalies");
            //Para hacer todos los hijos
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Anomalies");
            }

            if (GetComponent<StalkerBehaviour>())
            {
                GetComponent<StalkerBehaviour>().enabled = false;
            }
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Anomalies2");
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Anomalies2");
            }
        }
    }

    public void AnomalyRevealed()
    {
        if(revealType)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Default");
            }
            if(GetComponent<StalkerBehaviour>())
            {
                GetComponent<StalkerBehaviour>().enabled = true;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
