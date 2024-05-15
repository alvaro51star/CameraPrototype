using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class GrowlTrigger : MonoBehaviour
{
    [SerializeField] private Transform transformToGrowl;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.GetComponent<CatSounds>() || !other.gameObject.GetComponent<CatSounds>().isActiveAndEnabled)
            return;
        other.gameObject.transform.LookAt(transformToGrowl.position);
        other.gameObject.GetComponent<CatSounds>().CatGrowl();
        this.enabled = false;
    }
}
