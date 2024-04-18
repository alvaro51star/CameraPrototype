using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Door2 : DoubleAction
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private bool m_isLocked;
    [SerializeField] private AudioClip m_doorOpen;
    [SerializeField] private AudioClip m_doorClose;
    [SerializeField] private AudioClip m_doorLocked;
    [SerializeField] private bool m_doorInteract = false;


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
            m_doorInteract = true;
            m_animator.SetBool("AbrirPuerta", m_doorInteract);
        }
    }

    protected override void SecondActon()
    {
        base.SecondActon();
        AudioManager.Instance.ReproduceSound(m_doorClose);
        m_doorInteract = false;
        m_animator.SetBool("AbrirPuerta", m_doorInteract);
    }

    public void UnlockDoor()
    {
        m_isLocked = false;
    }
}
