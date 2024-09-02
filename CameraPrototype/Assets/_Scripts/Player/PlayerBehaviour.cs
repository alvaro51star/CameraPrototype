using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] Camera m_cam;
    private bool m_canInteract = false;
    private List<InteractiveObject> m_interactiveObjects = new List<InteractiveObject>();
    private InteractiveObject m_actualSimpleInteractiveObject;
    private InteractiveObject m_actualInputInteractiveObject;
    private bool m_hasCameraEquiped = false;
    private bool m_isReading;
    private bool m_isDoor;
    private bool m_isLockedDoor;
    private bool m_isCat;
    private bool m_isRedBeard;

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
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(m_actualInputInteractiveObject is null)
                print("actualInteractiveObject is null");
            else
            {
                print("actualInteractiveObject: " + m_actualInputInteractiveObject.gameObject.name);
            }
        }
        if (m_interactiveObjects.Count == 0) return;
        CheckObjectListNull();
        CheckActualInputInteractiveObject();
    }

    private void NotUsingCamera()
    {
        m_hasCameraEquiped = false;
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

    public bool GetIsReading()
    {
        return m_isReading;
    }

    public bool GetCanTakePicture()
    {
        return m_hasCameraEquiped;
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactiveObject = other.GetComponent<InteractiveObject>();
        if (interactiveObject is null || interactiveObject.enabled != true) return;
        if (interactiveObject.gameObject.layer is 6 or 7) return;
        interactiveObject.SwitchIsInArea(true);
        if (interactiveObject.GetNeedsButton())
        {
            m_interactiveObjects.Add(interactiveObject);
        }
        else
        {
            m_actualSimpleInteractiveObject = interactiveObject;
            SimpleInteraction();
        }
    }

    private void IsBesideInteractableObject()
    {
        m_canInteract = true;
        if (UIManager.instance.GetIsGamePaused() || m_isReading) return;
        UIManager.instance.SetInteractionText(true, m_actualInputInteractiveObject.GetInteractionScript().GetInteractionText());
        if (m_isDoor)
        {
            if (m_isLockedDoor)
            {
                UIManager.instance.InteractionAvialable(true, true, false);
            }
            else
            {
                UIManager.instance.InteractionAvialable(true, false, false);
            }

        }
        else if (!m_isDoor && m_isCat)
        {
            UIManager.instance.InteractionAvialable(true, false, true);

        }
        else if (!m_isDoor && !m_isCat)
        {
            UIManager.instance.InteractionAvialable(true, false, false);
            if (m_isRedBeard)
            {
                UIManager.instance.ShowInput(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("Nombre del objeto: " + other.gameObject.name);
        var interactiveObject = other.GetComponent<InteractiveObject>();
        if (interactiveObject is null || !m_interactiveObjects.Contains(interactiveObject)) return;
        interactiveObject.SwitchIsInArea(false);
        m_interactiveObjects.RemoveAt(m_interactiveObjects.IndexOf(interactiveObject));

        if (m_actualInputInteractiveObject == interactiveObject)
        {
            m_actualInputInteractiveObject = null;
            StopInteracting();
        }
        CheckObjectListNull();
    }

    private void StopInteracting()
    {
        m_canInteract = false;
        m_isLockedDoor = false;
        m_isDoor = false;
        m_isCat = false;
        m_isRedBeard = false;
        UIManager.instance.InteractionAvialable(false, false, false);
        UIManager.instance.SetInteractionText(false, "");
    }

    private void SimpleInteraction()
    {
        m_actualSimpleInteractiveObject.Interact(this.gameObject);
        m_actualSimpleInteractiveObject = null;
    }

    public void InputInteraction()
    {
        if (!m_canInteract || m_actualInputInteractiveObject is null || m_isReading) return;
        m_actualInputInteractiveObject.Interact(gameObject);
    }

    private void CheckActualInputInteractiveObject()
    {
        InteractiveObject nearestInteractiveObject = null;
        float nearestAngle = 100;
        var i = 0;

        for (i = 0; i < m_interactiveObjects.Count; i++)
        {
            if (i == 0)
            {
                nearestInteractiveObject = m_interactiveObjects[0];
            }

            var priorVectorDirector = nearestInteractiveObject.transform.position - m_cam.transform.position;
            var actualVectorDirector = m_interactiveObjects[i].m_interactionPivot.transform.position - m_cam.transform.position;
            var priorAngle = Vector3.Angle(m_cam.transform.forward, priorVectorDirector);
            var actualAngle = Vector3.Angle(m_cam.transform.forward, actualVectorDirector);

            if (!(actualAngle <= priorAngle)) continue;
            nearestInteractiveObject = m_interactiveObjects[i];
            nearestAngle = actualAngle;
        }

        if (nearestAngle <= nearestInteractiveObject.GetInteractionAngle())
        {
            m_actualInputInteractiveObject = nearestInteractiveObject;


            if (m_hasCameraEquiped || m_isReading) return;
            if (m_actualInputInteractiveObject.GetComponent<Interaction_Door>() is not null)
            {
                m_isDoor = true;
                m_isLockedDoor = m_actualInputInteractiveObject.GetComponent<Interaction_Door>().GetDiscoveredLocked();
            }
            if (m_actualInputInteractiveObject.GetComponent<Interaction_Cat>() is not null)
            {
                m_isCat = true;
            }
            if (m_actualInputInteractiveObject.GetComponent<Interaction_RedBeard>() is not null)
            {
                m_isRedBeard = true;
            }
            IsBesideInteractableObject();
        }

        else
        {
            m_actualInputInteractiveObject = null;
            StopInteracting();
        }


    }

    private void CheckObjectListNull()
    {
        for (int i = 0; i < m_interactiveObjects.Count; i++)
        {
            if (m_interactiveObjects[i] is null || m_interactiveObjects[i].gameObject.activeSelf == false || !m_interactiveObjects[i].GetIsInArea())
            {
                m_interactiveObjects.RemoveAt(i);
            }
        }

        if (m_actualInputInteractiveObject is null || m_actualInputInteractiveObject.gameObject.activeSelf 
            || m_actualInputInteractiveObject.enabled || m_actualInputInteractiveObject.GetIsInArea()) return;
        m_actualInputInteractiveObject = null;
        StopInteracting();
    }
}
