using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Figure : AffectsIndirectly
{
    protected override void FirstAction()
    {
        m_GO_targetObject.GetComponent<InteractiveObject>().enabled = true;
        m_GO_targetObject.GetComponent<Interaction_CajaArena>().SetFigurePicked(true);
    }
}
