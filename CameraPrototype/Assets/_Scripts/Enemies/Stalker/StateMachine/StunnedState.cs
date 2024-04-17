using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StunnedState : State
{
    private NavMeshAgent navMesh;

    [SerializeField] private AnimationClip stunnedAnimation;

    float currentTime;
    [SerializeField] private float maxTimeStunned = 3f;

    public override void Enter()
    {
        //maxTimeStunned = stunnedAnimation.length;
        //stalkerBehaviour.isStunned = true;
        isComplete = false;
        animator.Play("Stun");
        currentTime = 0f;
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
        navMesh.isStopped = false;
        isComplete = false;
        stalkerBehaviour.isStunned = false;
    }


    public void SetUp(NavMeshAgent navMesh, Animator animator, StalkerBehaviour stalkerBehaviour)
    {
        this.navMesh = navMesh;
        this.animator = animator;
        this.stalkerBehaviour = stalkerBehaviour;
    }
}
