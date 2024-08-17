using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Notas : InteractionScript
{
    //Variables 
    [SerializeField] string m_noteText;

    //Integraci�n FMOD
    [SerializeField] private string m_rutaEventoFMOD; //Aqu� necesita un sonido de papel


    public override void Action(GameObject player)
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.paper /*, this.transform.position */);

        UIManager.instance.ActivateNote(m_noteText);
        EventManager.OnIsReading?.Invoke();
    }
}
