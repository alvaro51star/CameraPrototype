using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_StoryBook : InteractionScript
{
    //Variables 
    [SerializeField] private AudioClip m_paperSound;

    public override void Action(GameObject player)
    {
        AudioManager.Instance.ReproduceSound(m_paperSound);
        UIManager.instance.StoryBook();
        EventManager.OnIsReading?.Invoke();
    }
}
