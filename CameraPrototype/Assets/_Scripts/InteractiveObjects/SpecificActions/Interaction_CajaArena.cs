using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_CajaArena : AffectsIndirectly
{
    //Variables
    [SerializeField] protected bool m_appears;
    [SerializeField] private bool m_figurePicked;
    [SerializeField] private GameObject m_figure;
    //Integración FMOD
    [SerializeField] private string m_rutaEventoFMODArena;

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
            AudioManager.Instance.PlayOneShot(FMODEvents.instance.sandBox /*, this.transform.position */);

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
