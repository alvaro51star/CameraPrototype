using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIBehaviour : MonoBehaviour
{
    [SerializeField] private PatrolRoutes patrolRoutes;
    [SerializeField] private PatrolRoute patrolRoute;
    [SerializeField] private float
        patrolSpeed,
        chaseSpeed,
        detectionRange,
        distanceToChangePoint;
    
    private NavMeshAgent m_agent;
    private Transform m_target;

    private List<Transform> m_patrolPoints;
    private int m_currentPatrolPoint;

    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        InitializeAgent();
        InitializePatrolPoints();
    }

    private void InitializeAgent()
    {
        SetAgentPatrolSpeed();
    }

    private void InitializePatrolPoints()
    {
        m_patrolPoints = patrolRoutes.GetPatrolRoute(patrolRoute);
    }

    public void ResetAgentPath()
    {
        m_agent.ResetPath();
    }

    public void SetAgentSpeed(float speed)
    {
        m_agent.speed = speed;
    }

    public void SetAgentPatrolSpeed()
    {
        SetAgentSpeed(patrolSpeed);
    }
    
    public void SetAgentChaseSpeed()
    {
        SetAgentSpeed(chaseSpeed);
    }

    public void SetAgentTarget(Transform target)
    {
        m_target = target;
    }

    public void SetNextPatrolPoint()
    {
        if (m_patrolPoints.Count == 0) return;
        
        SetAgentTarget(m_patrolPoints[(m_currentPatrolPoint++ % m_patrolPoints.Count)]);
    }

    public void SetMovingDestination()
    {
        if (m_target == null) return;
        m_agent.SetDestination(m_target.position);
    }

    public void GoToNextPoint()
    {
        SetNextPatrolPoint();
        m_agent.SetDestination(m_target.position);
    }

    /// <summary>
    /// Returns if the AI has reached it's waypoint
    /// </summary>
    /// <returns>
    /// true: The waypoint has been reached <br></br>
    /// false: The agent is still on path
    /// </returns>
    public bool CheckNextPoint()
    {
        return !m_agent.pathPending && m_agent.remainingDistance < distanceToChangePoint;
    }
    
}
