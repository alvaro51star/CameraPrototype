using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {            
            uIManager.ActivateLoseMenu();
            GameManager.Instance.CopyTimeToClipboard();
        }//
    }
}
