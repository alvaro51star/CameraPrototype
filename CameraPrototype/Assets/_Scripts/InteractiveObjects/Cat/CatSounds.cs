using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSounds : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnCatPetted += OnCatPetted;
    }

    private void OnDisable()
    {
        EventManager.OnCatPetted -= OnCatPetted;
    }

    private void OnCatPetted()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.meow /*, this.transform.position*/);
    }

    public void CatGrowl()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.growl /*, this.transform.position*/);
    }

}
