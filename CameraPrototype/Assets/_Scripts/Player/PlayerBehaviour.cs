using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Variables
    private bool m_canInteract = false;
    private InteractiveObject m_interactingObject;
    private bool m_canTakePicture = false;

    private void OnEnable()
    {
        EventManager.OnUsingCamera += OnUsingCamera;
        EventManager.OnNotUsingCamera += OnNotUsingCamera;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= OnUsingCamera;
        EventManager.OnNotUsingCamera -= OnNotUsingCamera;
    }
    
    private void OnNotUsingCamera()
    {
        m_canTakePicture = false;
    }

    private void OnUsingCamera()
    {
        m_canTakePicture = true;
    
    }

    public bool GetCanTakePicture()
    {
        return m_canTakePicture;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_interactingObject = other.GetComponent<InteractiveObject>();
        if (m_interactingObject != null)
        {
            m_canInteract = true;
            if (!m_interactingObject.GetNeedsButton())
            {
                Interaction();
            }
            else
            {
                UIManager.instance.ShowInput(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        m_canInteract = false;
        m_interactingObject = null;
        UIManager.instance.ShowInput(false);
    }

    public void Interaction()
    {
        if (m_canInteract && m_interactingObject != null)
        {
            m_interactingObject.Interact(this.gameObject);
        }
    }
}
