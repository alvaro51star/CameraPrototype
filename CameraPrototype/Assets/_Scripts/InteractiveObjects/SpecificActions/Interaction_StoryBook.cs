using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_StoryBook : InteractionScript
{
    //Variables 
    //Integración FMOD
    [SerializeField] private string m_rutaEventoFMOD; //Sonido de papel

    public override void Action(GameObject player)
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.paper /*, this.transform.position */);
        //Linea de FMOD
        UIManager.instance.StoryBook();
        EventManager.OnIsReading?.Invoke();
    }
}
