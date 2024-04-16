using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Notas : InteractionScript
{
    //Variables 
    [SerializeField] string m_noteText;

    public override void Action(GameObject player)
    {
        UIManager.instance.ActivateNote(m_noteText);
        EventManager.OnIsReading?.Invoke();
    }
}
