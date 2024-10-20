using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StunnedState : State
{
    [SerializeField] private NavMeshAgent navMesh;

    [SerializeField] private AnimationClip stunnedAnimation;

    float currentTime;
    [SerializeField] private float maxTimeStunned = 3f;

    [SerializeField] private float secondsToAdd = 0.5f;

    public override void Enter()
    {
        animtr_animator.enabled = true;
        m_stalkerBehaviour.isChasingPlayer = false;
        m_stateName = "Stunned";
        EventManager.OnStatusChange?.Invoke(m_stateName);

        navMesh.enabled = true;
        
        if (m_stalkerBehaviour.currentTimeLooked >= m_stalkerBehaviour.maxTimeLooked)
        {
            AddSecondsToEnemyBar();
        }
        
        isComplete = false;
        animtr_animator.Play("Stun");
        currentTime = 0f;
        navMesh.speed = 0f;
        navMesh.isStopped = true;
    }

    public override void Do()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTimeStunned)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {
        navMesh.speed = 3.5f;
        navMesh.isStopped = false;
        isComplete = false;
        m_stalkerBehaviour.isStunned = false;
        m_stalkerBehaviour.lastState = m_stalkerBehaviour.stunnedState;
    }


    public void SetUp(NavMeshAgent navMesh, Animator animator, StalkerBehaviour stalkerBehaviour)
    {
        this.navMesh = navMesh;
        this.animtr_animator = animator;
        this.m_stalkerBehaviour = stalkerBehaviour;
    }

    private void AddSecondsToEnemyBar()
    {
        m_stalkerBehaviour.currentTimeLooked = m_stalkerBehaviour.maxTimeLooked - secondsToAdd;
        EventManager.OnTimeAdded?.Invoke(m_stalkerBehaviour.currentTimeLooked, m_stalkerBehaviour.maxTimeLooked);
    }
}
