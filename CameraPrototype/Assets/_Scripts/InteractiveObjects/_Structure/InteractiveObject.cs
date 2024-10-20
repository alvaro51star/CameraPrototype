using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Serialization;

public class InteractiveObject : MonoBehaviour, I_InteractableObjects
{
    //Variables
    [SerializeField] private bool m_isNeedsButton;
    [SerializeField] private EventReference m_interactiveObject;
    [SerializeField] private float m_interactionAngle;
    [SerializeField] private InteractionScript m_interactionScript;
    public Transform m_tf_interactionPivot;
    private bool m_isInArea;

    public OutlineFx.OutlineFx outlineComponent;

    #region Getters and Setters
        public bool GetNeedsButton()
        {
            return m_isNeedsButton;
        }
        
        public bool GetIsInArea()
        {
            return m_isInArea;
        }
        
        public float GetInteractionAngle()
        {
            return m_interactionAngle;
        }
        
        public InteractionScript GetInteractionScript()
        {
            return m_interactionScript;
        }
    #endregion
    
    private void Start()
    {
        outlineComponent = GetComponentInChildren<OutlineFx.OutlineFx>(true);
        
        m_interactionScript = GetComponent<InteractionScript>();
        if (m_tf_interactionPivot == null)
        {
            m_tf_interactionPivot = transform;
        }
    }

    //Custom
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
