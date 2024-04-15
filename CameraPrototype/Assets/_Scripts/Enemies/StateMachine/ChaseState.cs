using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private GameObject player;

    public override void Enter()
    {
        //animator.Play("ChaseAnimation");
    }

    public override void Exit()
    {
        
    }

    public override void Do()
    {
        Chase();
    }

    private void Chase()
    {
        navMesh.SetDestination(player.transform.position);
    }

    
}
