using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Door : DoubleAction
{
    //Variables 
    [SerializeField] private Animator m_animator;
    [SerializeField] private bool m_isLocked;

    protected override void FirstAction()
    {
        if (m_isLocked)
        {
            //Mensaje de UY ESTÁ CERRADA
            print("toyCerrada");
        }

        else
        {
            base.FirstAction();
            //animación en un sentido. Con trigger al animator
            print("Meabro");
        }
    }

    protected override void SecondActon()
    {
        base.SecondActon();
        //animación en otro sentido. Con trigger al animator
        print("MeCierro");
    }

    public void UnlockDoor()
    {
        print("MeUnlockeo");
        m_isLocked = false;
    }
}
