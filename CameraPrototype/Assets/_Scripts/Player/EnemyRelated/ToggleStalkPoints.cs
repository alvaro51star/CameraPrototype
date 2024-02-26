using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleStalkPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<StalkPointBehaviour>(out StalkPointBehaviour stalkPoint))
        {
            Debug.Log("Localizao");
            stalkPoint.TogglePosition(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<StalkPointBehaviour>(out StalkPointBehaviour stalkPoint))
        {
            Debug.Log("Perdido");
            stalkPoint.TogglePosition(false);
        }
    }
}
