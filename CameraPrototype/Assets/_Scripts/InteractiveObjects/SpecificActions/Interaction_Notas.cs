using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Notas : InteractionScript
{
    //Variables 
    [SerializeField] string m_noteText;

    public override void Action()
    {
        UIManager.instance.ActivateNote(m_noteText);
        EventManager.IsReading?.Invoke();
    }
}
