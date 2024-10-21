using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class OutOfSightState : State
{
    private float m_currentTime = 0;

    [FormerlySerializedAs("_enemy")] [FormerlySerializedAs("enemy")] [SerializeField] private GameObject m_go_enemy;

    [FormerlySerializedAs("_navMeshAgent")] [SerializeField] private NavMeshAgent m_NavMAg_navMeshAgent;
    

    [FormerlySerializedAs("tpPoint")] [SerializeField] private Transform m_tf_tpPoint;

    [FormerlySerializedAs("timeToBeOutInLevel0")] [SerializeField] private float m_timeToBeOutInLevel0 = 30f;
    [FormerlySerializedAs("timeToBeOutInLevel1")] [SerializeField] private float m_timeToBeOutInLevel1 = 20f;
    [FormerlySerializedAs("timeToBeOutInLevel2")] [SerializeField] private float m_timeToBeOutInLevel2 = 10f;


    public override void Enter()
    {
        animtr_animator.enabled = false;
        m_stalkerBehaviour.isChasingPlayer = false;
        m_stateName = "OutOfSight";
        EventManager.OnStatusChange?.Invoke(m_stateName);
        m_NavMAg_navMeshAgent.isStopped = true;
        m_NavMAg_navMeshAgent.speed = 0f;

        isComplete = false;
        m_currentTime = 0;
        m_NavMAg_navMeshAgent.enabled = false;
        m_go_enemy.transform.position = m_tf_tpPoint.position;

    }

    public override void Do()
    {
        m_currentTime += Time.deltaTime;

        if (LevelManager.instance.intensityLevel == 0)
        {
            if (m_currentTime < m_timeToBeOutInLevel0)
            {
                return;
            }
            
            isComplete = true;
        }
        else if (LevelManager.instance.intensityLevel == 1)
        {
            if (m_currentTime < m_timeToBeOutInLevel1)
            {
                return;
            }
            isComplete = true;
        }
        else if (LevelManager.instance.intensityLevel == 2)
        {
            if (m_currentTime < m_timeToBeOutInLevel2)
            {
                return;
            }
            isComplete = true;
        }
        else
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {
        m_currentTime = 0;
        m_NavMAg_navMeshAgent.enabled = true;
        m_NavMAg_navMeshAgent.speed = m_stalkerBehaviour.enemySpeed;
        m_NavMAg_navMeshAgent.isStopped = false;
        m_stalkerBehaviour.lastState = m_stalkerBehaviour.outOfSightState;
    }

    public void SetUp(GameObject enemy, StalkerBehaviour stalkerBehaviour, NavMeshAgent navMeshAgent)
    {
        this.m_go_enemy = enemy;
        this.m_stalkerBehaviour = stalkerBehaviour;
        m_NavMAg_navMeshAgent = navMeshAgent;
    }
}
