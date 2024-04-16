using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour, IInteractiveObject
{
    //Variables
    [SerializeField] private bool m_needsButton;
    [SerializeField] Interactions m_interactionType;
    [SerializeField] private InteractionScript m_interactionScript;

    private void Start()
    {
        m_interactionScript = GetComponent<InteractionScript>();
        if (m_interactionScript != null)
        {
            print("Igotit)");
        }
    }

    public bool GetNeedsButton()
    {
        return m_needsButton;
    }

    public void Interact()
    {
        m_interactionScript.Action();
    }
}
