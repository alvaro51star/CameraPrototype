using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ChaseState : State
{

    [FormerlySerializedAs("navMesh")] [SerializeField] private NavMeshAgent m_NavMAg_navMesh;
    [FormerlySerializedAs("player")] [SerializeField] private GameObject m_go_player;

    [FormerlySerializedAs("enteredAnimation")] public bool isHasEnteredAnimation = false;



    public override void Enter()
    {
        animtr_animator.enabled = true;
        m_stateName = "Chase";
        EventManager.OnStatusChange?.Invoke(m_stateName);
        m_NavMAg_navMesh.enabled = true;
        m_NavMAg_navMesh.speed = m_stalkerBehaviour.enemySpeed;
        m_NavMAg_navMesh.isStopped = false;

        isComplete = false;

        animtr_animator.Play("Run");
        isHasEnteredAnimation = true;

        m_stalkerBehaviour.ActivateCollision();
    }

    public override void Exit()
    {
        isHasEnteredAnimation = false;
        isComplete = false;
        m_stalkerBehaviour.lastState = m_stalkerBehaviour.chaseState;
    }

    public override void Do()
    {
        Chase();
    }

    public void SetUp(NavMeshAgent navMeshAgent, GameObject player, Animator animator, StalkerBehaviour stalkerBehaviour)
    {
        m_NavMAg_navMesh = navMeshAgent;
        this.m_go_player = player;
        this.animtr_animator = animator;
        this.m_stalkerBehaviour = stalkerBehaviour;
    }

    private void Chase()
    {
        if (m_NavMAg_navMesh.isActiveAndEnabled)
        {
            m_NavMAg_navMesh.SetDestination(m_go_player.transform.position);
        }
    }
}
