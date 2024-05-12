using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] float m_minAngleToScreenCenter;
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

    private void Update()
    {
        if (m_interactiveObjects.Count != 0)
        {
            print("checking)");
            CheckActualInputInteractiveObject();
        }
    }

    private void NotUsingCamera()
    {
        m_hasCameraEquiped = false;
        //if (m_interactiveObjects != null)
        //{
        //    IsBesideInteractableObject();
        //}
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
            if (!m_hasCameraEquiped && !interactiveObject.m_isAnomaly)
            {
                if (interactiveObject.GetNeedsButton())
                {
                    m_interactiveObjects.Add(interactiveObject);
                    IsBesideInteractableObject();
                }
                else
                {
                    m_actualSimpleInteractiveObject = interactiveObject;
                    SimpleInteraction();
                }
            }
        }
    }

    private void IsBesideInteractableObject()
    {
        m_canInteract = true;
        UIManager.instance.ShowInput(true);
    }

    private void OnTriggerExit(Collider other)
    {
        InteractiveObject interactiveObject = other.GetComponent<InteractiveObject>();
        if (interactiveObject != null && m_interactiveObjects.Contains(interactiveObject))
        {
            m_interactiveObjects.RemoveAt(m_interactiveObjects.IndexOf(interactiveObject));

            if (m_actualInputInteractiveObject == interactiveObject)
            {
                m_actualInputInteractiveObject = null;
            }
            StopInteracting();
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
        if (m_actualInputInteractiveObject == null)
        {
            print("wtf");
        }

        if (m_canInteract && m_actualInputInteractiveObject != null && !m_isReading)
        {
            print("interacts");
            m_actualInputInteractiveObject.Interact(this.gameObject);
        }
    }

    public void CheckActualInputInteractiveObject()
    {
        InteractiveObject nearestIteractiveObject = null;
        float nearestAngle = 100;
        int i = 0;

        for (i = 0; i < m_interactiveObjects.Count; i++)
        {
            if (i == 0)
            {
                nearestIteractiveObject = m_interactiveObjects[0];
            }

            Vector3 priorVectorDirector = nearestIteractiveObject.transform.position - m_cam.transform.position;
            Vector3 actualVectorDirector =  m_interactiveObjects[i].m_interactionPivot.transform.position - m_cam.transform.position;
            float priorAngle = Vector3.Angle(m_cam.transform.forward, priorVectorDirector);
            float actualAngle = Vector3.Angle(m_cam.transform.forward, actualVectorDirector);

            if (actualAngle <= priorAngle)
            {
                nearestIteractiveObject = m_interactiveObjects[i];
                nearestAngle = actualAngle;
            }
        }

        if (nearestAngle <= m_minAngleToScreenCenter)
        {
            m_actualInputInteractiveObject = nearestIteractiveObject;
        }

        else
        {
            print("null");
            m_actualInputInteractiveObject = null;
        }
    }
}
