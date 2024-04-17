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
            m_animator.SetTrigger("Abrir");
        }
    }

    protected override void SecondActon()
    {
        base.SecondActon();
        AudioManager.Instance.ReproduceSound(m_doorClose);
        m_animator.SetTrigger("Cerrar");
    }

    public void UnlockDoor()
    {
        m_isLocked = false;
    }
}
