using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Serialization;

public class InteractiveObject : MonoBehaviour, I_InteractableObjects
{
    //Variables
    [FormerlySerializedAs("m_needsButton")] [SerializeField] private bool m_isNeedsButton;
    [SerializeField] private InteractionScript m_interactionScript;
    [FormerlySerializedAs("interactiveObject")] [SerializeField] private EventReference m_interactiveObject;
    [SerializeField] float m_interactionAngle;
    [FormerlySerializedAs("m_interactionPivot")] public Transform m_tf_interactionPivot;
    private bool m_isInArea;

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
        m_interactionScript = GetComponent<InteractionScript>();
        if (m_tf_interactionPivot == null)
        {
            m_tf_interactionPivot = transform;
        }
    }

    //Custom
    public void Interact(GameObject player)
    {
        m_interactionScript.Action(player);
        AudioManager.Instance.PlayOneShot(m_interactiveObject /*, this.transform.position */);
    }

    public void SwitchIsInArea(bool mode)
    {
        m_isInArea = mode;
    }
    
}
