using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    public bool isComplete  { get; protected set; } = false;

    protected float startTime;

    public float time => Time.time - startTime;

    protected Animator animator;

    protected StalkerBehaviour stalkerBehaviour;

    public virtual void Enter() { }

    public virtual void Do() { }

    public virtual void FixedDo() { }

    public virtual void Exit() { }
}