using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_StoryBook : InteractionScript
{
    //Variables 
    [SerializeField] private AudioClip m_paperSound;
    //Integración FMOD
    [SerializeField] private string m_rutaEventoFMOD; //Sonido de papel

    public override void Action(GameObject player)
    {
        AudioManager.Instance.ReproduceSound(m_paperSound);
        //Linea de FMOD
        UIManager.instance.StoryBook();
        EventManager.OnIsReading?.Invoke();
    }
}
