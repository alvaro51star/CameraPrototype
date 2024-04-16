using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Variables
    private bool m_canInteract = false;
    private InteractiveObject m_interactingObject;
    private bool m_hasCameraEquiped = false;

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
        m_hasCameraEquiped = false;
        if (m_interactingObject != null)
        {
            IsBesideInteractableObject();
        }
    }

    private void OnUsingCamera()
    {
        m_hasCameraEquiped = true;
        StopInteracting();
    }

    public bool GetCanTakePicture()
    {
        return m_hasCameraEquiped;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_interactingObject = other.GetComponent<InteractiveObject>();
        if (m_interactingObject != null && !m_hasCameraEquiped)
        {
            IsBesideInteractableObject();
        }
    }


    private void IsBesideInteractableObject()
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

    private void OnTriggerExit(Collider other)
    {
        StopInteracting();
        m_interactingObject = null;
    }

    private void StopInteracting()
    {
        m_canInteract = false;
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
