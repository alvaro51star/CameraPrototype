using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public abstract class State : MonoBehaviour
{
    public bool isComplete  { get; set; } = false;

    protected float m_startTime;

    public float time => Time.time - m_startTime;

    [FormerlySerializedAs("animator")] public Animator animtr_animator;

    protected string m_stateName;

    protected StalkerBehaviour m_stalkerBehaviour;

    public virtual void Enter() { }

    public virtual void Do() { }

    public virtual void FixedDo() { }

    public virtual void Exit() { }
}
