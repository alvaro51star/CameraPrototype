using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OutOfSightState : State
{
    private float currentTime = 0;

    [SerializeField] private GameObject enemy;

    [SerializeField] private Transform tpPoint;

    [SerializeField] private float timeToBeOutInLevel0 = 30f;
    [SerializeField] private float timeToBeOutInLevel1 = 20f;
    [SerializeField] private float timeToBeOutInLevel2 = 10f;


    public override void Enter()
    {
        stateName = "OutOfSight";
        EventManager.OnStatusChange?.Invoke(stateName);

        isComplete = false;
        currentTime = 0;

        enemy.transform.position = tpPoint.position;
        enemy.GetComponent<NavMeshAgent>().isStopped = true;
        enemy.GetComponent<NavMeshAgent>().speed = 0f;

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
        enemy.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public void SetUp(GameObject enemy, StalkerBehaviour stalkerBehaviour)
    {
        this.enemy = enemy;
        this.stalkerBehaviour = stalkerBehaviour;
    }
}
