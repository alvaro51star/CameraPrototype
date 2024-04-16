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
        maxTimeStunned = stunnedAnimation.length;
        //TODO animator.Play("StunnedAnimation");
        currentTime = 0f;
        isComplete = false;
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
    }


    public void SetUp(NavMeshAgent navMesh)
    {
        this.navMesh = navMesh;
    }
}
