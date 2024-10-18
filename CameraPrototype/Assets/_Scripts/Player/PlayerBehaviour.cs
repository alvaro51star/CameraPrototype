using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] Camera m_cam;
    private bool m_isCanInteract = false;
    private List<InteractiveObject> m_L_interactiveObjects = new List<InteractiveObject>();
    private InteractiveObject m_actualInputInteractiveObject, m_actualSimpleInteractiveObject;
    private bool m_isCameraEquiped, m_isReading, m_isDoor, m_isLockedDoor, m_isCat, m_isRedBeard;

    private void OnEnable()
    {
        EventManager.OnUsingCamera += UsingCamera;
        EventManager.OnNotUsingCamera += NotUsingCamera;
        EventManager.OnIsReading += IsReading;
        EventManager.OnStopReading += StopReading;
        EventManager.OnInteractiveObjectDisabled += CheckObjectListNull;
    }

    private void OnDisable()
    {
        EventManager.OnUsingCamera -= UsingCamera;
        EventManager.OnNotUsingCamera -= NotUsingCamera;
        EventManager.OnIsReading -= IsReading;
        EventManager.OnStopReading -= StopReading;
        EventManager.OnInteractiveObjectDisabled -= CheckObjectListNull;
    }

    private void Update()
    {
        if (m_L_interactiveObjects.Count == 0) return;
        CheckObjectListNull();
        CheckActualInputInteractiveObject();
    }

    #region Events Methods
    
        private void NotUsingCamera()
        {
            m_isCameraEquiped = false;
        }
        
        private void UsingCamera()
        {
            m_isCameraEquiped = true;
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
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        var interactiveObject = other.GetComponent<InteractiveObject>();
        if (interactiveObject is null || interactiveObject.enabled != true) return;
        if (interactiveObject.gameObject.layer is 6 or 7) return;
        interactiveObject.SwitchIsInArea(true);
        if (interactiveObject.GetNeedsButton())
        {
            m_L_interactiveObjects.Add(interactiveObject);
        }
        else
        {
            m_actualSimpleInteractiveObject = interactiveObject;
            SimpleInteraction();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        var interactiveObject = other.GetComponent<InteractiveObject>();
        if (interactiveObject is null || !m_L_interactiveObjects.Contains(interactiveObject)) return;
        interactiveObject.SwitchIsInArea(false);
        m_L_interactiveObjects.RemoveAt(m_L_interactiveObjects.IndexOf(interactiveObject));

        if (m_actualInputInteractiveObject == interactiveObject)
        {
            m_actualInputInteractiveObject = null;
            StopInteracting();
        }
        CheckObjectListNull();
    }

    //Custom
    private void IsBesideInteractableObject()
    {
        m_isCanInteract = true;
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

    private void StopInteracting()
    {
        m_isCanInteract = false;
        m_isLockedDoor = false;
        m_isDoor = false;
        m_isCat = false;
        m_isRedBeard = false;
        UIManager.instance.InteractionAvialable(false, false, false);
        UIManager.instance.SetInteractionText(false, "");
    }

    private void SimpleInteraction()
    {
        m_actualSimpleInteractiveObject.I_Interact(this.gameObject);
        m_actualSimpleInteractiveObject = null;
    }

    public void InputInteraction()
    {
        if (!m_isCanInteract || m_actualInputInteractiveObject is null || m_isReading) return;
        m_actualInputInteractiveObject.I_Interact(gameObject);
    }

    private void CheckActualInputInteractiveObject()
    {
        InteractiveObject nearestInteractiveObject = null;
        float nearestAngle = 100;
        var i = 0;

        if (m_L_interactiveObjects.Count == 0) return;

        for (i = 0; i < m_L_interactiveObjects.Count; i++)
        {
            if (i == 0)
            {
                nearestInteractiveObject = m_L_interactiveObjects[0];
            }

            var priorVectorDirector = nearestInteractiveObject.transform.position - m_cam.transform.position;
            var actualVectorDirector = m_L_interactiveObjects[i].tf_interactionPivot.transform.position - m_cam.transform.position;
            var priorAngle = Vector3.Angle(m_cam.transform.forward, priorVectorDirector);
            var actualAngle = Vector3.Angle(m_cam.transform.forward, actualVectorDirector);

            if (!(actualAngle <= priorAngle)) continue;
            nearestInteractiveObject = m_L_interactiveObjects[i];
            nearestAngle = actualAngle;
        }

        if (!nearestInteractiveObject.isActiveAndEnabled || nearestInteractiveObject is null) return;

        if (nearestAngle <= nearestInteractiveObject.GetInteractionAngle())
        {
            m_actualInputInteractiveObject = nearestInteractiveObject;


            if (m_isCameraEquiped || m_isReading) return;
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
        for (int i = 0; i < m_L_interactiveObjects.Count; i++)
        {
            if (m_L_interactiveObjects[i] is null || m_L_interactiveObjects[i].gameObject.activeSelf == false || !m_L_interactiveObjects[i].GetIsInArea())
            {
                m_L_interactiveObjects.RemoveAt(i);
            }
        }
        
        if (m_actualInputInteractiveObject is null || !m_actualInputInteractiveObject.gameObject.activeSelf || 
            !m_actualInputInteractiveObject.enabled || !m_actualInputInteractiveObject.GetIsInArea())
        {    
            m_actualInputInteractiveObject = null;
            StopInteracting();
        }
    }
}
