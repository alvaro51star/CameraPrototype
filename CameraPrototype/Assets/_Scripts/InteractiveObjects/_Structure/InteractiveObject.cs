using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour, I_InteractableObjects
{
    //Variables
    [SerializeField] private bool m_needsButton;
    [SerializeField] private InteractionScript m_interactionScript;
    [SerializeField] private AudioClip m_interactSound;
    [SerializeField] float m_interactionAngle;
    public Transform m_interactionPivot;

    //Integraci�n FMOD
    [SerializeField] private string m_rutaEventoFMODInteraccion; //cambiable en editor para cada objeto. Es el sonido que hace al cogerlo usarlo etc.
                                                                 //Sonidos custimizados en cada script (ej en puerta)
    private void Start()
    {
        m_interactionScript = GetComponent<InteractionScript>();
        if (m_interactionPivot == null)
        {
            m_interactionPivot = transform;
        }
    }

    public bool GetNeedsButton()
    {
        return m_needsButton;
    }

    public void Interact(GameObject player)
    {
        m_interactionScript.Action(player);
        if (m_interactSound != null)
        {
            AudioManager.Instance.ReproduceSound(m_interactSound);
            //L�neaFMOD
        }

    }

    public InteractionScript GetInteractionScript()
    {
        return m_interactionScript;
    }

    public float GetInteractionAngle()
    {
        return m_interactionAngle;
    }
}
