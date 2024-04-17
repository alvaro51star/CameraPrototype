using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{

    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;

    public bool enteredAnimation = false;

    public override void Enter()
    {
        isComplete = false;

        if (enteredAnimation == false)
        {
            animator.Play("Run");
        }
        enteredAnimation = true;
    }

    public override void Exit()
    {
        enteredAnimation = false;
        isComplete = false;
    }

    public override void Do()
    {
        Chase();
    }

    public void SetUp(NavMeshAgent navMeshAgent, GameObject player, Animator animator, StalkerBehaviour stalkerBehaviour)
    {
        navMesh = navMeshAgent;
        this.player = player;
        this.animator = animator;
        this.stalkerBehaviour = stalkerBehaviour;
    }

    private void Chase()
    {
        navMesh.SetDestination(player.transform.position);
    }

}
