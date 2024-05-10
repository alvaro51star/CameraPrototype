using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class StalkState : State
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Renderer objectMesh;

    public float currentTime;
    [SerializeField] private float timeBeforeChangingPoint = 5f;

    [SerializeField] private float timeToCompleteStalk_Level0 = 20f;
    [SerializeField] private float timeToCompleteStalk_Level1 = 10f;
    [SerializeField] private float timeToCompleteStalk_Level2 = 5f;

    private const float timeLevel0 = 20f;
    private const float timeLevel1 = 10f;
    private const float timeLevel2 = 5f;


    public override void Enter()
    {
        stateName = "Stalk";
        EventManager.OnStatusChange?.Invoke(stateName);

        enemy.GetComponent<NavMeshAgent>().destination = enemy.transform.position;
        enemy.GetComponent<NavMeshAgent>().isStopped = true;

        currentTime = 0f;
        animator.Play("Idle");
        isComplete = false;

        if (!objectMesh.isVisible)
        {
            TPToNextPosition();
        }

        ResetTimer();
        CalculateTimes();
    }



    public override void Exit()
    {
        isComplete = false;
        enemy.GetComponent<NavMeshAgent>().isStopped = false;
    }

    public override void Do()
    {
        Stalk();
    }

    private void Stalk()
    {
        currentTime += Time.deltaTime;

        if (LevelManager.instance.intensityLevel == 0)
        {
            if (currentTime >= timeToCompleteStalk_Level0 && !objectMesh.isVisible)
            {
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 1)
        {
            if (currentTime >= timeToCompleteStalk_Level1 && !objectMesh.isVisible)
            {
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 2)
        {
            if (currentTime >= timeToCompleteStalk_Level2 && !objectMesh.isVisible)
            {
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 3)
        {
            if (currentTime >= timeBeforeChangingPoint && !objectMesh.isVisible)
            {
                TPToNextPosition();
                ResetTimer();
            }
        }


    }

    private void TPToNextPosition()
    {
        if (StalkPointsManager.instance.activeStalkPoints.Count <= 0)
        {
            return;
        }
        enemy.transform.position = StalkPointsManager.instance.activeStalkPoints[Random.Range(0, StalkPointsManager.instance.activeStalkPoints.Count)].transform.position;
        //transform.LookAt(player.transform);
    }

    private void ResetTimer()
    {
        currentTime = 0;
    }

    private void CalculateTimes()
    {
        timeToCompleteStalk_Level0 = Random.Range(timeLevel0 - timeLevel0 * 0.25f, timeLevel0 + timeLevel0 * 0.25f);
        timeToCompleteStalk_Level1 = Random.Range(timeLevel1 - timeLevel1 * 0.25f, timeLevel1 + timeLevel1 * 0.25f);
        timeToCompleteStalk_Level2 = Random.Range(timeLevel2 - timeLevel2 * 0.25f, timeLevel2 + timeLevel2 * 0.25f);
    }

    public void SetUp(GameObject enemy, Renderer objectMesh, Animator animator, StalkerBehaviour stalkerBehaviour)
    {
        this.enemy = enemy;
        this.objectMesh = objectMesh;
        this.animator = animator;
        this.stalkerBehaviour = stalkerBehaviour;
    }
}
