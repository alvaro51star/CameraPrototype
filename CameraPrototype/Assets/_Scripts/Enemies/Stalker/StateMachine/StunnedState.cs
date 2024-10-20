using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class StunnedState : State
{
    [FormerlySerializedAs("navMesh")] [SerializeField] private NavMeshAgent m_NavMag_navMesh;

    [FormerlySerializedAs("stunnedAnimation")] [SerializeField] private AnimationClip m_AnimClip_stunnedAnimation;

    float m_currentTime;
    [FormerlySerializedAs("maxTimeStunned")] [SerializeField] private float m_maxTimeStunned = 3f;

    [FormerlySerializedAs("secondsToAdd")] [SerializeField] private float m_secondsToAdd = 0.5f;

    public override void Enter()
    {
        animtr_animator.enabled = true;
        m_stalkerBehaviour.isChasingPlayer = false;
        m_stateName = "Stunned";
        EventManager.OnStatusChange?.Invoke(m_stateName);

        m_NavMag_navMesh.enabled = true;
        
        if (m_stalkerBehaviour.currentTimeLooked >= m_stalkerBehaviour.maxTimeLooked)
        {
            AddSecondsToEnemyBar();
        }
        
        isComplete = false;
        animtr_animator.Play("Stun");
        m_currentTime = 0f;
        m_NavMag_navMesh.speed = 0f;
        m_NavMag_navMesh.isStopped = true;
    }

    public override void Do()
    {
        m_currentTime += Time.deltaTime;
        if (m_currentTime >= m_maxTimeStunned)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {
        m_NavMag_navMesh.speed = 3.5f;
        m_NavMag_navMesh.isStopped = false;
        isComplete = false;
        m_stalkerBehaviour.isStunned = false;
        m_stalkerBehaviour.lastState = m_stalkerBehaviour.stunnedState;
    }


    public void SetUp(NavMeshAgent navMesh, Animator animator, StalkerBehaviour stalkerBehaviour)
    {
        this.m_NavMag_navMesh = navMesh;
        this.animtr_animator = animator;
        this.m_stalkerBehaviour = stalkerBehaviour;
    }

    private void AddSecondsToEnemyBar()
    {
        m_stalkerBehaviour.currentTimeLooked = m_stalkerBehaviour.maxTimeLooked - m_secondsToAdd;
        EventManager.OnTimeAdded?.Invoke(m_stalkerBehaviour.currentTimeLooked, m_stalkerBehaviour.maxTimeLooked);
    }
}
