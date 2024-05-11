using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AnomaliesData))]
public class AnomalyBehaviour : MonoBehaviour
{
    private AnomaliesData anomaliesData;
    private void OnEnable()
    {
        EventManager.OnTakingPhoto += OnTakingPhoto;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= OnTakingPhoto;
    }

    private void Start()
    {
        anomaliesData = GetComponent<AnomaliesData>();
    }
    private void OnTakingPhoto()
    {
        if (gameObject == null)
            return;
        if(gameObject.GetComponent<Renderer>() == null)
            return;        
        if(!gameObject.GetComponent<Renderer>().isVisible)
            return;
        if (anomaliesData.isActiveAndEnabled)
        {
            RevealAnomaly();
            anomaliesData.enabled = false;
        }
    }
    private void RevealAnomaly()
    {
        if (anomaliesData.revealType)
        {
            if (transform.CompareTag("Enemy"))
            {
                gameObject.GetComponent<StalkerBehaviour>().ActivateCollision();
            }

            gameObject.layer = LayerMask.NameToLayer("Default");
            transform.ChangeLayersRecursively(LayerMask.NameToLayer("Default"));
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Default");
            }
            if (GetComponent<StalkerBehaviour>())
            {
                GetComponent<StalkerBehaviour>().enabled = true;
            }
            else if (GetComponent<Llave>())
            {
                GetComponent<Llave>().enabled = true;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

        this.enabled = false;
    }

}
