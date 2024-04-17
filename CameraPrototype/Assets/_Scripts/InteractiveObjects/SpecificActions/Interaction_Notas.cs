using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Notas : InteractionScript
{
    //Variables 
    [SerializeField] string m_noteText;
    [SerializeField] private AudioClip m_paperSound;

    public override void Action(GameObject player)
    {
        AudioManager.Instance.ReproduceSound(m_paperSound);
        UIManager.instance.ActivateNote(m_noteText);
        EventManager.OnIsReading?.Invoke();
    }
}
