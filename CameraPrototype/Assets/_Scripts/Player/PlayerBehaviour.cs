using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] float m_distanceToScreenCenter;
    [SerializeField] Camera m_cam;
    private bool m_canInteract = false;
    private List<InteractiveObject> m_interactiveObjects = new List<InteractiveObject>();
    private InteractiveObject m_actualSimpleInteractiveObject;
    private InteractiveObject m_actualInputInteractiveObject;
    private bool m_hasCameraEquiped = false;
    private bool m_isReading;

    private void OnEnable()
    {
        EventManager.OnUsingCamera += UsingCamera;
        EventManager.OnNotUsingCamera += NotUsingCamera;
        EventManager.OnIsReading += IsReading;
        EventManager.OnStopReading += StopReading;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= UsingCamera;
        EventManager.OnNotUsingCamera -= NotUsingCamera;
        EventManager.OnIsReading -= IsReading;
        EventManager.OnStopReading -= StopReading;
    }
    
    private void NotUsingCamera()
    {
        m_hasCameraEquiped = false;
        if (m_interactiveObjects != null)
        {
            IsBesideInteractableObject();
        }
    }

    private void UsingCamera()
    {
        m_hasCameraEquiped = true;
        StopInteracting();
    }

    private void IsReading()
    {
        m_isReading = true;
        EventManager.OnIsReading -= IsReading;
        EventManager.OnStopReading += StopReading;
    }

    private void StopReading()
    {
        m_isReading = false;
        EventManager.OnStopReading -= StopReading;
        EventManager.OnIsReading += IsReading;
    }

    public bool GetCanTakePicture()
    {
        return m_hasCameraEquiped;
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractiveObject interactiveObject = other.GetComponent<InteractiveObject>();
        if (interactiveObject != null)
        {
            m_interactiveObjects.Add(interactiveObject);
            if (!m_hasCameraEquiped && 
                m_interactiveObjects[m_interactiveObjects.LastIndexOf(interactiveObject)].gameObject.layer != 6 && 
                m_interactiveObjects[m_interactiveObjects.LastIndexOf(interactiveObject)].gameObject.layer != 7)
            {
                if (m_interactiveObjects[m_interactiveObjects.LastIndexOf(interactiveObject)].GetNeedsButton())
                {
                    IsBesideInteractableObject();
                }
                else
                {
                    m_actualSimpleInteractiveObject = m_interactiveObjects[m_interactiveObjects.LastIndexOf(interactiveObject)];
                    m_interactiveObjects.RemoveAt(m_interactiveObjects.LastIndexOf(interactiveObject));
                    SimpleInteraction();
                }
            }
            print(m_cam.WorldToScreenPoint(interactiveObject.transform.position));
        }

    }


    private void IsBesideInteractableObject()
    {
        m_canInteract = true;
        CheckActualInputInteractiveObject();
        UIManager.instance.ShowInput(true);
    }

    private void OnTriggerExit(Collider other)
    {
        InteractiveObject interactiveObject = other.GetComponent<InteractiveObject>();
        if (interactiveObject != null)
        {
            m_interactiveObjects.RemoveAt(m_interactiveObjects.LastIndexOf(interactiveObject));

            if (m_actualInputInteractiveObject == interactiveObject)
            {
                m_actualInputInteractiveObject = null;
                StopInteracting();
            }
        }
    }

    private void StopInteracting()
    {
        m_canInteract = false;
        UIManager.instance.ShowInput(false);
    }

    public void SimpleInteraction()
    {
        if (m_canInteract && m_interactiveObjects != null && !m_isReading)
        {
            m_actualSimpleInteractiveObject.Interact(this.gameObject);
            m_actualSimpleInteractiveObject = null;
            StopInteracting();
        }
    }

    public void InputInteraction()
    {
        if (m_canInteract && m_actualInputInteractiveObject != null && !m_isReading)
        {
            m_actualInputInteractiveObject.Interact(this.gameObject);
        }
    }

    public void CheckActualInputInteractiveObject()
    {
        List<InteractiveObject> temporalInteractiveObjects = new List<InteractiveObject>();
        for (int i = 0; i < m_interactiveObjects.Count; i++)
        {
            if (m_interactiveObjects[i].m_renderer.isVisible)
            {
                temporalInteractiveObjects.Add(m_interactiveObjects[i]);
            }
        }

        int nearestObjectIndex = 0;
        Vector2 mindistancetoScreenCenter = new Vector2(Screen.height / 4, Screen.width / 4); 
        List<InteractiveObject> temporalInteractiveObjects2 = new List<InteractiveObject>();
        for (int j = 0; j < temporalInteractiveObjects.Count; j++)
        {
            Vector3 distToLeftDownCorner = m_cam.WorldToScreenPoint(temporalInteractiveObjects[j].transform.position);
            //if(distToLeftDownCorner)
        }
    }
}
