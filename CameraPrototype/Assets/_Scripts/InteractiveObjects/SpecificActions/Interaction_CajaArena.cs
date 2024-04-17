using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_CajaArena : AffectsIndirectly
{
    //Variables
    [SerializeField] protected bool m_appears;
    [SerializeField] private bool m_figurePicked;

    public override void Action(GameObject player)
    {
        if (m_figurePicked)
        {
            base.Action(player);
        }
    }

    protected override void FirstAction()
    {
        if (m_figurePicked)
        {
            ChangeActiveMode(m_targetObject, m_appears);
        }
    }

    public void SetFigurePicked(bool mode)
    {
        m_figurePicked = mode;
    }
}
