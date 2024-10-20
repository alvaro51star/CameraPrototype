using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class OutOfSightState : State
{
    private float currentTime = 0;

    [FormerlySerializedAs("enemy")] [SerializeField] private GameObject _enemy;

    [SerializeField] private NavMeshAgent _navMeshAgent;
    

    [SerializeField] private Transform tpPoint;

    [SerializeField] private float timeToBeOutInLevel0 = 30f;
    [SerializeField] private float timeToBeOutInLevel1 = 20f;
    [SerializeField] private float timeToBeOutInLevel2 = 10f;


    public override void Enter()
    {
        animator.enabled = false;
        stalkerBehaviour.isChasingPlayer = false;
        stateName = "OutOfSight";
        EventManager.OnStatusChange?.Invoke(stateName);
        _navMeshAgent.isStopped = true;
        _navMeshAgent.speed = 0f;

        isComplete = false;
        currentTime = 0;
        _navMeshAgent.enabled = false;
        _enemy.transform.position = tpPoint.position;

    }

    public override void Do()
    {
        currentTime += Time.deltaTime;

        if (LevelManager.instance.intensityLevel == 0)
        {
            if (currentTime < timeToBeOutInLevel0)
            {
                return;
            }
            
            isComplete = true;
        }
        else if (LevelManager.instance.intensityLevel == 1)
        {
            if (currentTime < timeToBeOutInLevel1)
            {
                return;
            }
            isComplete = true;
        }
        else if (LevelManager.instance.intensityLevel == 2)
        {
            if (currentTime < timeToBeOutInLevel2)
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
        currentTime = 0;
        _navMeshAgent.enabled = true;
        _navMeshAgent.speed = stalkerBehaviour.enemySpeed;
        _navMeshAgent.isStopped = false;
        stalkerBehaviour.lastState = stalkerBehaviour.outOfSightState;
    }

    public void SetUp(GameObject enemy, StalkerBehaviour stalkerBehaviour, NavMeshAgent navMeshAgent)
    {
        this._enemy = enemy;
        this.stalkerBehaviour = stalkerBehaviour;
        _navMeshAgent = navMeshAgent;
    }
}
