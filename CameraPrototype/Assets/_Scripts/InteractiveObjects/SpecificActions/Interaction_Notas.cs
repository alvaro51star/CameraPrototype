using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Notas : InteractionScript
{
    //Variables 
    [SerializeField] string m_noteText;
    [SerializeField] private AudioClip m_paperSound;

    //Integración FMOD
    [SerializeField] private string m_rutaEventoFMOD; //Aquí necesita un sonido de papel


    public override void Action(GameObject player)
    {
        AudioManager.Instance.ReproduceSound(m_paperSound);
        //Linea de FMOD

        UIManager.instance.ActivateNote(m_noteText);
        EventManager.OnIsReading?.Invoke();
    }
}
