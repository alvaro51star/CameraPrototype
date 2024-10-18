using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Interaction_Notas : InteractionScript
{
    //Variables 
    [SerializeField] string m_str_noteText;

    public override void Action(GameObject player)
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.paper /*, this.transform.position */);

        UIManager.instance.ActivateNote(m_str_noteText);
        EventManager.OnIsReading?.Invoke();
    }
}
