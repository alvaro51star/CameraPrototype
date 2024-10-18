using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy: MonoBehaviour
{
    public bool isActive;
    public virtual void KillPlayer() { }
}
