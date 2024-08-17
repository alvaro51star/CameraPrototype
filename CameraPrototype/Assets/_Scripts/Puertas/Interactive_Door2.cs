using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Door2 : DoubleAction
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private bool m_isLocked;
    [SerializeField] private bool m_doorInteract = false;


    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    protected override void FirstAction()
    {
        if (m_isLocked)
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.instance.doorLocked /*, this.transform.position */);
        }

        else
        {
            base.FirstAction();
            AudioManager.Instance.PlayOneShot(FMODEvents.instance.openDoor /*, this.transform.position */);
            m_doorInteract = true;
            m_animator.SetBool("AbrirPuerta", m_doorInteract);
        }
    }

    protected override void SecondActon()
    {
        base.SecondActon();
        AudioManager.Instance.PlayOneShot(FMODEvents.instance.closeDoor /*, this.transform.position */);
        m_doorInteract = false;
        m_animator.SetBool("AbrirPuerta", m_doorInteract);
    }

    public void UnlockDoor()
    {
        m_isLocked = false;
    }
}
