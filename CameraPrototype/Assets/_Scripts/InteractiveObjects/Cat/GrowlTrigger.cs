using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class GrowlTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.GetComponent<CatSounds>() || !other.gameObject.GetComponent<CatSounds>().isActiveAndEnabled)
            return;
        other.gameObject.GetComponent<CatSounds>().CatGrowl();
        this.enabled = false;
    }
}
