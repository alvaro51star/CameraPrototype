using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State : MonoBehaviour
{
    public bool isComplete  { get; set; } = false;

    protected float startTime;

    public float time => Time.time - startTime;

    public Animator animator;

    protected string stateName;

    protected StalkerBehaviour stalkerBehaviour;

    public virtual void Enter() { }

    public virtual void Do() { }

    public virtual void FixedDo() { }

    public virtual void Exit() { }
}
