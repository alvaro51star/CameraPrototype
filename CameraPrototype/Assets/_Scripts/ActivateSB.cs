using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSB : MonoBehaviour
{
    public GameObject GO;

    public void Activate()
    {
        GO.SetActive(true);
    }

    public void Deactivate()
    {
        GO.SetActive(false);
    }
}
