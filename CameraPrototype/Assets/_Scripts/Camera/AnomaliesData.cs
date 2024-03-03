using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomaliesData : MonoBehaviour
{
    public bool revealType;//hay 2 tipos de anomalia, las que revelas objetos y las que quitas objetos
    void Start() //innecesario (esta ya puesto en editor), solo por si acaso
    {
        if(revealType)
        {
            gameObject.layer = LayerMask.NameToLayer("Anomalies");
        }

        else
        {
            gameObject.layer = LayerMask.NameToLayer("Anomalies2");
        }
    }

    public void AnomalyRevealed()
    {
        if(revealType)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
