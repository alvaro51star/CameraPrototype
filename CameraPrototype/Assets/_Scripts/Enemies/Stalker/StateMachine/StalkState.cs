using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private const float timeLevel0 = 15f;
    private const float timeLevel1 = 7.5f;
    private const float timeLevel2 = 3f;

    [SerializeField] private float maxDistanceToGrowlState = 2f;

    private bool hasBeenVisible = false;

    bool firstTimeEntered = false;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Transform rayPoint;
    private bool growlCalled = false;


    public override void Enter()
    {
        audioSource.Play();
        stateName = "Stalk";
        EventManager.OnStatusChange?.Invoke(stateName);

        enemy.GetComponent<NavMeshAgent>().destination = enemy.transform.position;
        enemy.GetComponent<NavMeshAgent>().isStopped = true;

        currentTime = 0f;
        animator.Play("Idle");
        isComplete = false;
        hasBeenVisible = false;

        if (firstTimeEntered)
        {
            if (stalkerBehaviour.lastState == stalkerBehaviour.outOfSightState)
            {
                TPToNextPosition();
            }
            if (stalkerBehaviour.lastState != null && stalkerBehaviour.states != stalkerBehaviour.stunnedState && stalkerBehaviour.states != stalkerBehaviour.stalkState)
                TPToNextPosition();
        }



        ResetTimer();
        CalculateTimes();
        firstTimeEntered = true;
    }



    public override void Exit()
    {
        audioSource.Stop();
        isComplete = false;
        hasBeenVisible = false;
        enemy.GetComponent<NavMeshAgent>().isStopped = false;
        stalkerBehaviour.lastState = stalkerBehaviour.stalkState;
    }

    public override void Do()
    {

        if (!growlCalled)
        {
            CheckDistance();
        }

        if (objectMesh.isVisible)
        {
            hasBeenVisible = true;
        }
        Stalk();
    }

    private void Stalk()
    {
        if (hasBeenVisible)
        {
            currentTime += Time.deltaTime;
        }

        if (LevelManager.instance.intensityLevel == 0)
        {
            if (currentTime >= timeToCompleteStalk_Level0 && !objectMesh.isVisible)
            {
                Debug.Log("Entrado en out of sight");
                stalkerBehaviour.OutOfSight();
                ResetTimer();
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 1)
        {
            if (currentTime >= timeToCompleteStalk_Level1 && !objectMesh.isVisible)
            {
                stalkerBehaviour.OutOfSight();
                ResetTimer();
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 2)
        {
            if (currentTime >= timeToCompleteStalk_Level2 && !objectMesh.isVisible)
            {
                stalkerBehaviour.OutOfSight();
                ResetTimer();
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 3)
        {
            if (currentTime >= timeBeforeChangingPoint && !objectMesh.isVisible)
            {
                if (TPToNextPosition())
                    ResetTimer();
            }
        }
    }

    private bool TPToNextPosition()
    {
        if (StalkPointsManager.instance.activeStalkPoints.Count <= 0)
        {
            return false;
        }

        Transform closestPosition;
        List<Transform> stalkPointsReachable = new();

        foreach (var stalkPoint in StalkPointsManager.instance.activeStalkPoints)
        {
            NavMeshPath path = new();
            if (enemy.GetComponent<NavMeshAgent>().CalculatePath(stalkPoint.transform.position, path))
            {
                stalkPointsReachable.Add(stalkPoint.transform);
            }
        }

        if (stalkPointsReachable.Count > 0)
        {
            closestPosition = stalkPointsReachable[0];
            foreach (var stalkPoint in stalkPointsReachable)
            {
                if (Vector3.Distance(stalkPoint.position, stalkerBehaviour.player.transform.position)
                    <= Vector3.Distance(closestPosition.position, stalkerBehaviour.player.transform.position))
                {
                    closestPosition = stalkPoint;
                }
            }
        }
        else
        {
            closestPosition = StalkPointsManager.instance.activeStalkPoints[Random.Range(0, StalkPointsManager.instance.activeStalkPoints.Count)].transform;
        }
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        enemy.transform.position = closestPosition.position;
        enemy.GetComponent<NavMeshAgent>().enabled = true;
        return true;
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

    private void CheckDistance()
    {
        Vector3 dir = stalkerBehaviour.player.transform.position - transform.position;
        Debug.DrawRay(transform.position, dir * maxDistanceToGrowlState, Color.red);
        if (Physics.Raycast(rayPoint.position, dir, out RaycastHit hit, maxDistanceToGrowlState))
        {

            if (hit.transform.gameObject.CompareTag("Player"))
            {
                growlCalled = true;
                Debug.Log("Choco con el player");
                stalkerBehaviour.Growl();
            }
        }
    }
}
