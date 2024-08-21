using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSounds : MonoBehaviour
{
    public void CatGrowl()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.growl /*, this.transform.position*/);
    }

}
