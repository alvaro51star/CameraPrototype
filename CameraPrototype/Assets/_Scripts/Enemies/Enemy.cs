using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Enemy: MonoBehaviour
{
    [FormerlySerializedAs("isActive")] public bool iIsActive;
    public virtual void KillPlayer() { }
}
