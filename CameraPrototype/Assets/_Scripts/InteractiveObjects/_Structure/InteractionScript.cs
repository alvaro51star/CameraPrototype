using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionScript : MonoBehaviour
{

    [SerializeField] protected string m_interactText;
    public abstract void Action(GameObject player);

    public string GetInteractionText()
    {
        return m_interactText;
    }
}
