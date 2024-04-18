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
    private int m_doorInteract = 0;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    protected override void FirstAction()
    {
        if (m_isLocked)
        {
            AudioManager.Instance.ReproduceSound(m_doorLocked);
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

    public void UnlockDoor()
    {
        m_isLocked = false;
    }
}
