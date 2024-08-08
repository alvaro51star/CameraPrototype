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
        animator.enabled = true;
        stateName = "Chase";
        EventManager.OnStatusChange?.Invoke(stateName);
        navMesh.enabled = true;
        navMesh.speed = stalkerBehaviour.enemySpeed;
        navMesh.isStopped = false;

        isComplete = false;

        animator.Play("Run");
        enteredAnimation = true;

        stalkerBehaviour.ActivateCollision();
    }

    public override void Exit()
    {
        enteredAnimation = false;
        isComplete = false;
        stalkerBehaviour.lastState = stalkerBehaviour.chaseState;
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
        if (navMesh.isActiveAndEnabled)
        {
            navMesh.SetDestination(player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stalkerBehaviour.EnterState(stalkerBehaviour.playerCatchState);
            stalkerBehaviour.playerCatched = true;
            isComplete = true;
        }
    }

}
