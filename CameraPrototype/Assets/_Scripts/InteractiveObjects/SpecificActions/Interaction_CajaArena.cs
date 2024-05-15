using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_CajaArena : AffectsIndirectly
{
    //Variables
    [SerializeField] protected bool m_appears;
    [SerializeField] private bool m_figurePicked;
    [SerializeField] private GameObject m_figure;
    [SerializeField] private AudioClip m_arenaSound;

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
            AudioManager.Instance.ReproduceSound(m_arenaSound);
            ChangeActiveMode(m_targetObject, m_appears);
            ChangeActiveMode(m_figure, true);
            SetFigurePicked(false);
            GetComponent<InteractiveObject>().enabled = false;

        }
    }

    public void SetFigurePicked(bool mode)
    {
        m_figurePicked = mode;
    }
}
