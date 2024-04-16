using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_ActivateDeactivate
{
    public void ChangeActiveMode(GameObject gameObjectToDeactivate, bool mode)
    {
        gameObjectToDeactivate.SetActive(mode);
    }
}
