using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StalkPointsManager : MonoBehaviour
{
    public static StalkPointsManager instance;

    [FormerlySerializedAs("activeStalkPoints")] public List<StalkPointBehaviour> L_activeStalkPoints = new();

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
            L_activeStalkPoints.Add(stalkPoint);
        }
        else
        {
            L_activeStalkPoints.Remove(stalkPoint);
        }
    }
}
