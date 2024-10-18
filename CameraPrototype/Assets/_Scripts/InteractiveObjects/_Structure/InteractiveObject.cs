using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Serialization;

public class InteractiveObject : MonoBehaviour, I_InteractableObjects
{
    //Variables
    [SerializeField] private bool m_isNeedsButton;
    [SerializeField] private InteractionScript m_interactionScript;
    [SerializeField] private EventReference m_interactiveObject;
    [SerializeField] float m_interactionAngle;
    public Transform tf_interactionPivot;
    private bool m_isInArea;

    #region Getters
        public bool GetNeedsButton()
        {
            return m_isNeedsButton;
        }
        
        public InteractionScript GetInteractionScript()
        {
            return m_interactionScript;
        }
        
        public float GetInteractionAngle()
        {
            return m_interactionAngle;
        }
        
        public bool GetIsInArea()
        {
            return m_isInArea;
        }
    #endregion
    
    private void Start()
    {
        m_interactionScript = GetComponent<InteractionScript>();
        if (tf_interactionPivot == null)
        {
            tf_interactionPivot = transform;
        }
    }

    public void I_Interact(GameObject player)
    {
        m_interactionScript.Action(player);
        AudioManager.Instance.PlayOneShot(m_interactiveObject /*, this.transform.position */);
    }

    public void SwitchIsInArea(bool mode)
    {
        m_isInArea = mode;
    }
}
