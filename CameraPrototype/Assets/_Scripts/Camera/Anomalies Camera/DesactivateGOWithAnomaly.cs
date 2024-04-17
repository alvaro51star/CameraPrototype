using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateGOWithAnomaly : MonoBehaviour
{
    [SerializeField] private GameObject goToDesactivate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !this.gameObject.GetComponent<AnomaliesData>().enabled)
        {
            goToDesactivate.SetActive(false);
        }
    }
}
