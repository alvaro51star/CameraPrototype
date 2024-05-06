using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour, I_InteractableObjects
{
    //Variables
    [SerializeField] private bool m_needsButton;
    [SerializeField] private InteractionScript m_interactionScript;
    [SerializeField] private AudioClip m_interactSound;
    [SerializeField] public Renderer m_renderer;

    private void Start()
    {
        m_interactionScript = GetComponent<InteractionScript>();
    }

    public bool GetNeedsButton()
    {
        return m_needsButton;
    }

    public void Interact(GameObject player)
    {
        m_interactionScript.Action(player);
        AudioManager.Instance.ReproduceSound(m_interactSound);
    }
}
