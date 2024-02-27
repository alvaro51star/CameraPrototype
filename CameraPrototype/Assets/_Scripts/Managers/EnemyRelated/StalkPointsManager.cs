using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkPointsManager : MonoBehaviour
{
    public static StalkPointsManager instance;

    public List<StalkPointBehaviour> activeStalkPoints = new();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void ModifyStalkPointList(StalkPointBehaviour stalkPoint, bool modifier)
    {
        if (modifier)
        {
            activeStalkPoints.Add(stalkPoint);
        }
        else
        {
            activeStalkPoints.Remove(stalkPoint);
        }
    }
}
