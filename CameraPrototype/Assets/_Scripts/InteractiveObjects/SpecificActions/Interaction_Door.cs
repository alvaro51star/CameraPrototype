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
    private int m_doorInteract = 0;
    private bool m_discoveredLocked = false;

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
            //m_animator.SetTrigger("Abrir");
            m_doorInteract = 2;
            m_animator.SetInteger("Abrir", m_doorInteract);
            
        }
    }

    protected override void SecondActon()
    {
        base.SecondActon();
        AudioManager.Instance.ReproduceSound(m_doorClose);
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
}
