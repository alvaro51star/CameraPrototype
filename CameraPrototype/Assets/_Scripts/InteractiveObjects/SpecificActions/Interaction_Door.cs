using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Door : DoubleAction
{
    //Variables 
    [SerializeField] private Animator m_animator;
    [SerializeField] private bool m_isLocked; 
    [SerializeField] private AudioClip m_doorOpen;
    [SerializeField] private AudioClip m_doorClose;
    [SerializeField] private AudioClip m_doorLocked;
    [SerializeField] private bool m_isPuertaPrincipal;
    [SerializeField] private Collider m_collider;
    [SerializeField] private bool m_isCatDoor;
    private int m_doorInteract = 0;
    private bool m_discoveredLocked = false;

    //Integración FMOD
    [SerializeField] private string m_rutaEventoFMOD; //Hacen falta 3 sonidos: abrir, cerrar y bloqueado. Tal vez también uno al desbloquear la puerta

    protected override void Start()
    {
        base.Start();
        m_animator = GetComponent<Animator>();
    }
    protected override void FirstAction()
    {
        if (m_isLocked)
        {
            AudioManager.Instance.ReproduceSound(m_doorLocked);
            //LineaFMOD: puerta bloqueada.

            if (!m_discoveredLocked && !m_isPuertaPrincipal)
            {
                print("discovered door locjed");
                m_discoveredLocked = true;
            }
        }

        else
        {
            base.FirstAction();
            AudioManager.Instance.ReproduceSound(m_doorOpen);
            //LineaFMOD: puerta abierta.

            m_doorInteract = 2;
            m_animator.SetInteger("Abrir", m_doorInteract);
            if (m_isCatDoor)
            {
                EventManager.OnDoorOpened?.Invoke();
                m_isCatDoor = false;
            }
            
        }
    }

    protected override void SecondActon()
    {
        base.SecondActon();
        //m_animator.SetTrigger("Cerrar");
        m_doorInteract = 1;
        m_animator.SetInteger("Abrir", m_doorInteract);
    }

    public void SetlockDoor(bool mode)
    {
        if (!mode)
        {
            if (!m_isPuertaPrincipal)
            {
                m_discoveredLocked = false;
            }
            m_isLocked = false;
        }
        else
        {
            SecondActon();
            m_isLocked = true;
        }
    }

    public bool GetDiscoveredLocked()
    {
        return m_discoveredLocked;
    }

    public void SetCollisionFalse()
    {
        m_collider.enabled = false;
    }

    public void SetCollisionTrue()
    {
        m_collider.enabled = true;
    }

    public void PlayCloseSound()
    {
        if (m_doorInteract == 1)
        {
            AudioManager.Instance.ReproduceSound(m_doorClose);
            //LineaFMOD: puerta cerrada.
        }
    }
}
