using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfSightState : State
{
    private float currentTime = 0;

    [SerializeField] private GameObject enemy;

    [SerializeField] private Transform tpPoint;


    public override void Enter()
    {
        stateName = "OutOfSight";
        EventManager.OnStatusChange?.Invoke(stateName);

        isComplete = false;

        enemy.transform.position = tpPoint.position;
    }

    public override void Do()
    {
        currentTime += Time.deltaTime;

        if (LevelManager.instance.intensityLevel == 0)
        {
            if (currentTime < LevelManager.instance.timeToEnterFirstLevel)
            {
                return;
            }
            isComplete = true;
        }
        else if (LevelManager.instance.intensityLevel == 1)
        {
            if (currentTime < LevelManager.instance.timeToEnterSecondLevel)
            {
                return;
            }
            isComplete = true;
        }
        else if (LevelManager.instance.intensityLevel == 2)
        {
            if (currentTime < LevelManager.instance.timeToEnterThirdLevel)
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
    }

    public void SetUp(GameObject enemy, StalkerBehaviour stalkerBehaviour)
    {
        this.enemy = enemy;
        this.stalkerBehaviour = stalkerBehaviour;
    }
}
