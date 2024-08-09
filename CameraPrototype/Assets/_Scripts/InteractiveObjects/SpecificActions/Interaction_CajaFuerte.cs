using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_CajaFuerte : AffectsIndirectly
{
    //Variables 
    [SerializeField] private string m_correctCode;
    [SerializeField] private int m_maxCodeNumber;
    [SerializeField] private AudioClip[] m_keySounds;
    [SerializeField] private AudioClip m_wrongSound;
    [SerializeField] private AudioClip m_rightSound;
    [SerializeField] private AudioClip m_safeUnlockSound;
    [SerializeField] private AudioClip m_safeOpen;
    [SerializeField] private Animator anim;
    private bool m_isOpen = false;
    private string m_actualCode = "";

    //Integración FMOD
    [SerializeField] private string m_rutaEventoFMOD; //Hace falta sonidos paralas teclas, un sonido de codigo incorrecto, otro de codigo correcto, sonido de desbloquear caja fuerte
                                                      // y sonido de la puerta abriendose

    protected override void FirstAction()
    {
        if (!m_isOpen)
        {
            UIManager.instance.ShowSafe(true);
            EventManager.OnIsReading?.Invoke();
        }
    }

    public void AddActualCode(GameObject button)
    {
        ReproduceKeySound();
        if (m_actualCode.Length < m_maxCodeNumber)
        {
            m_actualCode += button.name;
        }
        UIManager.instance.ChangeCodeDisplay(m_actualCode);
        UIManager.instance.ShowLight(false, true);
    }

    public void ReproduceKeySound()
    {
        AudioClip clip = m_keySounds[Random.Range(0, m_keySounds.Length)];
        AudioManager.Instance.ReproduceSound(clip);

        //LineaFMOD para las teclas
    }

    public void DeleteActualCode()
    {
        ReproduceKeySound();
        m_actualCode = "";
        UIManager.instance.ChangeCodeDisplay(m_actualCode);
    }

    public void CheckCode()
    {
        ReproduceKeySound();
        if (m_actualCode == m_correctCode)
        {
            m_isOpen = true;
            UIManager.instance.ShowLight(true, false);

            AudioManager.Instance.ReproduceSound(m_rightSound);
            AudioManager.Instance.ReproduceSound(m_safeUnlockSound);
            AudioManager.Instance.ReproduceSound(m_safeOpen);
            //Lineas de FMOD para codigo correcto, desbloquear y abrir, aunque la de abrir se podría aparte pa hacerla con animación

            ChangeActiveMode(m_targetObject, true);
            GetComponent<InteractiveObject>().enabled = false;
            anim.SetInteger("Abrir", 1);
        }
        else
        {
            UIManager.instance.ShowLight(true, true);
            AudioManager.Instance.ReproduceSound(m_wrongSound);
            //Linea FMOD de sonido de codigo incorrecto
        }
    }
}
