using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Variables
    private bool m_canInteract = false;
    private InteractiveObject m_interactingObject;
    private void OnTriggerEnter(Collider other)
    {
        m_interactingObject = other.GetComponent<InteractiveObject>();
        m_canInteract = true;
        if (!m_interactingObject.GetNeedsButton())
        {
            Interaction();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        m_canInteract = false;
        m_interactingObject = null;
    }

    public void Interaction()
    {
        if (m_canInteract && m_interactingObject != null)
        {
            Debug.Log("HI");
            m_interactingObject.Interact();
        }
    }
}
