using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class InteractionScript : MonoBehaviour
{
    [SerializeField] protected string m_str_interactText;
    
    public string GetInteractionText()
    {
        return m_str_interactText;
    }
    
    public abstract void Action(GameObject player);

}
