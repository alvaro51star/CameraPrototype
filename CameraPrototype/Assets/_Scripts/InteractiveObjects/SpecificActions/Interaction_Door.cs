using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Interaction_Door : DoubleAction
{
    //Variables 
    [SerializeField] private Animator m_animatr;
    [SerializeField] private bool m_isLocked, m_isPuertaPrincipal, m_isCatDoor, m_needsDialogue; 
    [SerializeField] private Collider m_collider;
    [SerializeField] private TextForDialogue textForDialogue;
    private int m_doorInteract = 0;
    private bool m_isDiscoveredLocked = false;
    
    #region Setters and Getters
        public bool GetDiscoveredLocked()
        {
            return m_isDiscoveredLocked;
        }
        
        public void SetlockDoor(bool mode)
        {
            if (!mode)
            {
                if (!m_isPuertaPrincipal)
                {
                    m_isDiscoveredLocked = false;
                }
                m_isLocked = false;
            }
            else
            {
                SecondActon();
                m_isLocked = true;
            }
        }
        
        public void SetCollisionFalse()
        {
            m_collider.enabled = false;
        }
        
        public void SetCollisionTrue()
        {
            m_collider.enabled = true;
        }
    #endregion
    
    protected override void Start()
    {
        base.Start();
        m_animatr = GetComponent<Animator>();
        if (m_needsDialogue == true)
        {
            textForDialogue = GetComponent<TextForDialogue>();
        }
    }
    
    //Custom
    protected override void FirstAction()
    {
        if (m_isLocked)
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.instance.doorLocked /*, this.transform.position */);

            if (m_needsDialogue)
            {
                textForDialogue.StartDialogue();//provisional
            }
            if (!m_isDiscoveredLocked && !m_isPuertaPrincipal)
            {
                m_isDiscoveredLocked = true;
            }
        }

        else
        {
            base.FirstAction();
            AudioManager.Instance.PlayOneShot(FMODEvents.instance.openDoor /*, this.transform.position */);

            m_doorInteract = 2;
            m_animatr.SetInteger("Abrir", m_doorInteract);
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
        m_animatr.SetInteger("Abrir", m_doorInteract);
    }

    public void PlayCloseSound()
    {
        if (m_doorInteract == 1)
        {
            AudioManager.Instance.PlayOneShot(FMODEvents.instance.closeDoor /*, this.transform.position */);
            //LineaFMOD: puerta cerrada.
        }
    }
}
