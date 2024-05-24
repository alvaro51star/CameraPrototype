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
        Debug.Log("Chase state");
        stateName = "Chase";
        EventManager.OnStatusChange?.Invoke(stateName);
        if (navMesh.isActiveAndEnabled)
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
            stalkerBehaviour.states = stalkerBehaviour.playerCatchState;
            stalkerBehaviour.playerCatched = true;
            isComplete = true;
        }
    }

}
