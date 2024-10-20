using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using FMOD.Studio;



public class StalkState : State
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Renderer objectMesh;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    

    public float currentTime;
    [SerializeField] private float timeBeforeChangingPoint = 5f;

    [SerializeField] private float timeToCompleteStalk_Level0 = 20f;
    [SerializeField] private float timeToCompleteStalk_Level1 = 10f;
    [SerializeField] private float timeToCompleteStalk_Level2 = 5f;

    private const float timeLevel0 = 15f;
    private const float timeLevel1 = 7.5f;
    private const float timeLevel2 = 3f;

    public float maxDistanceToGrowlState = 2f;

    private bool hasBeenVisible = false;

    bool firstTimeEntered = false;

    [SerializeField] private Transform rayPoint;
    private bool growlCalled = false;

    // AUDIO
    private EventInstance stalkerBreathing;

    private void Start()
    {
        stalkerBreathing = AudioManager.Instance.CreateEventInstance(FMODEvents.instance.stalkerBreathingSound);
    }

    public override void Enter()
    {
        animtr_animator.enabled = true;

        PLAYBACK_STATE playbackState;
        stalkerBreathing.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            stalkerBreathing.start();

        m_stateName = "Stalk";
        EventManager.OnStatusChange?.Invoke(m_stateName);

        _navMeshAgent.destination = enemy.transform.position;
        _navMeshAgent.isStopped = true;

        currentTime = 0f;
        animtr_animator.Play("Idle");
        isComplete = false;
        hasBeenVisible = false;

        if (firstTimeEntered)
        {
            if (m_stalkerBehaviour.lastState == m_stalkerBehaviour.outOfSightState)
            {
                TPToNextPosition();
            }
            if (m_stalkerBehaviour.lastState != null && m_stalkerBehaviour.currentState != m_stalkerBehaviour.stunnedState && m_stalkerBehaviour.currentState != m_stalkerBehaviour.stalkState)
                TPToNextPosition();
        }



        ResetTimer();
        CalculateTimes();
        firstTimeEntered = true;
    }



    public override void Exit()
    {
        stalkerBreathing.stop(STOP_MODE.ALLOWFADEOUT);

        isComplete = false;
        hasBeenVisible = false;
        _navMeshAgent.isStopped = false;
        m_stalkerBehaviour.lastState = m_stalkerBehaviour.stalkState;
        growlCalled = false;
    }

    public override void Do()
    {

        if (!growlCalled && !m_stalkerBehaviour.isStunned)
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
                m_stalkerBehaviour.EnterState(m_stalkerBehaviour.outOfSightState);
                ResetTimer();
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 1)
        {
            if (currentTime >= timeToCompleteStalk_Level1 && !objectMesh.isVisible)
            {
                m_stalkerBehaviour.EnterState(m_stalkerBehaviour.outOfSightState);
                ResetTimer();
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 2)
        {
            if (currentTime >= timeToCompleteStalk_Level2 && !objectMesh.isVisible)
            {
                m_stalkerBehaviour.EnterState(m_stalkerBehaviour.outOfSightState);
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

        //Localiza los puntos a los que hay camino para llegar
        foreach (var stalkPoint in StalkPointsManager.instance.activeStalkPoints)
        {
            NavMeshPath path = new();
            if (_navMeshAgent.CalculatePath(stalkPoint.transform.position, path))
            {
                stalkPointsReachable.Add(stalkPoint.transform);
            }
        }

        //Si es mayor a 0 recorre el array y compara las distancias y se elige el mas cercano
        if (stalkPointsReachable.Count > 0)
        {
            closestPosition = stalkPointsReachable[0];
            foreach (var stalkPoint in stalkPointsReachable)
            {
                if (Vector3.Distance(stalkPoint.position, m_stalkerBehaviour.go_player.transform.position)
                    <= Vector3.Distance(closestPosition.position, m_stalkerBehaviour.go_player.transform.position))
                {
                    closestPosition = stalkPoint;
                }
            }
        }
        else //Si no se pilla uno random activo y fuera
        {
            closestPosition = StalkPointsManager.instance.activeStalkPoints[Random.Range(0, StalkPointsManager.instance.activeStalkPoints.Count)].transform;
        }

        _navMeshAgent.enabled = false;
        enemy.transform.position = closestPosition.position;
        _navMeshAgent.enabled = true;
        return true;
    }

    private void ResetTimer()
    {
        currentTime = 0;
    }

    //Calcular tiempo segun los niveles de intensidad y el tiempo maximo
    private void CalculateTimes()
    {
        timeToCompleteStalk_Level0 = Random.Range(timeLevel0 - timeLevel0 * 0.25f, timeLevel0 + timeLevel0 * 0.25f);
        timeToCompleteStalk_Level1 = Random.Range(timeLevel1 - timeLevel1 * 0.25f, timeLevel1 + timeLevel1 * 0.25f);
        timeToCompleteStalk_Level2 = Random.Range(timeLevel2 - timeLevel2 * 0.25f, timeLevel2 + timeLevel2 * 0.25f);
    }

    public void SetUp(GameObject enemy, Renderer objectMesh, Animator animator, StalkerBehaviour stalkerBehaviour, NavMeshAgent navMeshAgent)
    {
        this.enemy = enemy;
        this.objectMesh = objectMesh;
        this.animtr_animator = animator;
        this.m_stalkerBehaviour = stalkerBehaviour;
        _navMeshAgent = navMeshAgent;
    }

    //Se encarga de comparar las distancias por si se acerca demasiado pasa al estado de Growl
    private void CheckDistance()
    {
        if(m_stalkerBehaviour.isPlayerCatched)
            return;
        
        Vector3 dir = m_stalkerBehaviour.go_player.transform.position - transform.position;
        Debug.DrawRay(transform.position, dir * maxDistanceToGrowlState, Color.red);
        if (Physics.Raycast(rayPoint.position, dir, out RaycastHit hit, maxDistanceToGrowlState))
        {

            if (hit.transform.gameObject.CompareTag("Player"))
            {
                growlCalled = true;
                m_stalkerBehaviour.EnterState(m_stalkerBehaviour.growlState);
            }
        }
    }
}
