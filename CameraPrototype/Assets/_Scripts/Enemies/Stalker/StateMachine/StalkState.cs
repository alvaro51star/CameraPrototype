using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using FMOD.Studio;
using UnityEngine.Serialization;


public class StalkState : State
{
    [FormerlySerializedAs("enemy")] [SerializeField] private GameObject m_go_enemy;
    [FormerlySerializedAs("objectMesh")] [SerializeField] private Renderer m_rdr_objectMesh;
    [FormerlySerializedAs("_navMeshAgent")] [SerializeField] private NavMeshAgent m_NavMAg_navMeshAgent;
    

    public float currentTime;
    [FormerlySerializedAs("timeBeforeChangingPoint")] [SerializeField] private float m_timeBeforeChangingPoint = 5f;

    [FormerlySerializedAs("timeToCompleteStalk_Level0")] [SerializeField] private float m_timeToCompleteStalk_Level0 = 20f;
    [FormerlySerializedAs("timeToCompleteStalk_Level1")] [SerializeField] private float m_timeToCompleteStalk_Level1 = 10f;
    [FormerlySerializedAs("timeToCompleteStalk_Level2")] [SerializeField] private float m_timeToCompleteStalk_Level2 = 5f;

    private const float TIMELEVEL0 = 15f;
    private const float TIMELEVEL1 = 7.5f;
    private const float TIMELEVEL2 = 3f;

    public float maxDistanceToGrowlState = 2f;

    private bool m_isHasBeenVisible = false;

    bool m_isFirstTimeEntered = false;

    [FormerlySerializedAs("rayPoint")] [SerializeField] private Transform m_tf_rayPoint;
    private bool growlCalled = false;

    // AUDIO
    private EventInstance m_FM_stalkerBreathing;

    private void Start()
    {
        m_FM_stalkerBreathing = AudioManager.Instance.CreateEventInstance(FMODEvents.instance.stalkerBreathingSound);
    }

    public override void Enter()
    {
        animtr_animator.enabled = true;

        PLAYBACK_STATE playbackState;
        m_FM_stalkerBreathing.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            m_FM_stalkerBreathing.start();

        m_stateName = "Stalk";
        EventManager.OnStatusChange?.Invoke(m_stateName);

        m_NavMAg_navMeshAgent.destination = m_go_enemy.transform.position;
        m_NavMAg_navMeshAgent.isStopped = true;

        currentTime = 0f;
        animtr_animator.Play("Idle");
        isComplete = false;
        m_isHasBeenVisible = false;

        if (m_isFirstTimeEntered)
        {
            if (m_stalkerBehaviour.lastState == m_stalkerBehaviour.outOfSightState)
            {
                TPToNextPosition();
            }
            if (m_stalkerBehaviour.lastState && m_stalkerBehaviour.currentState != m_stalkerBehaviour.stunnedState && m_stalkerBehaviour.currentState != m_stalkerBehaviour.stalkState)
                TPToNextPosition();
        }



        ResetTimer();
        CalculateTimes();
        m_isFirstTimeEntered = true;
    }



    public override void Exit()
    {
        m_FM_stalkerBreathing.stop(STOP_MODE.ALLOWFADEOUT);

        isComplete = false;
        m_isHasBeenVisible = false;
        m_NavMAg_navMeshAgent.isStopped = false;
        m_stalkerBehaviour.lastState = m_stalkerBehaviour.stalkState;
        growlCalled = false;
    }

    public override void Do()
    {

        if (!growlCalled && !m_stalkerBehaviour.isStunned)
        {
            CheckDistance();
        }

        if (m_rdr_objectMesh.isVisible)
        {
            m_isHasBeenVisible = true;
        }
        Stalk();
    }

    private void Stalk()
    {
        if (m_isHasBeenVisible)
        {
            currentTime += Time.deltaTime;
        }

        if (LevelManager.instance.intensityLevel == 0)
        {
            if (currentTime >= m_timeToCompleteStalk_Level0 && !m_rdr_objectMesh.isVisible)
            {
                m_stalkerBehaviour.EnterState(m_stalkerBehaviour.outOfSightState);
                ResetTimer();
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 1)
        {
            if (currentTime >= m_timeToCompleteStalk_Level1 && !m_rdr_objectMesh.isVisible)
            {
                m_stalkerBehaviour.EnterState(m_stalkerBehaviour.outOfSightState);
                ResetTimer();
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 2)
        {
            if (currentTime >= m_timeToCompleteStalk_Level2 && !m_rdr_objectMesh.isVisible)
            {
                m_stalkerBehaviour.EnterState(m_stalkerBehaviour.outOfSightState);
                ResetTimer();
                isComplete = true;
            }
        }
        else if (LevelManager.instance.intensityLevel == 3)
        {
            if (currentTime >= m_timeBeforeChangingPoint && !m_rdr_objectMesh.isVisible)
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
            if (m_NavMAg_navMeshAgent.CalculatePath(stalkPoint.transform.position, path))
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

        m_NavMAg_navMeshAgent.enabled = false;
        m_go_enemy.transform.position = closestPosition.position;
        m_NavMAg_navMeshAgent.enabled = true;
        return true;
    }

    private void ResetTimer()
    {
        currentTime = 0;
    }

    //Calcular tiempo segun los niveles de intensidad y el tiempo maximo
    private void CalculateTimes()
    {
        m_timeToCompleteStalk_Level0 = Random.Range(TIMELEVEL0 - TIMELEVEL0 * 0.25f, TIMELEVEL0 + TIMELEVEL0 * 0.25f);
        m_timeToCompleteStalk_Level1 = Random.Range(TIMELEVEL1 - TIMELEVEL1 * 0.25f, TIMELEVEL1 + TIMELEVEL1 * 0.25f);
        m_timeToCompleteStalk_Level2 = Random.Range(TIMELEVEL2 - TIMELEVEL2 * 0.25f, TIMELEVEL2 + TIMELEVEL2 * 0.25f);
    }

    public void SetUp(GameObject enemy, Renderer objectMesh, Animator animator, StalkerBehaviour stalkerBehaviour, NavMeshAgent navMeshAgent)
    {
        this.m_go_enemy = enemy;
        this.m_rdr_objectMesh = objectMesh;
        this.animtr_animator = animator;
        this.m_stalkerBehaviour = stalkerBehaviour;
        m_NavMAg_navMeshAgent = navMeshAgent;
    }

    //Se encarga de comparar las distancias por si se acerca demasiado pasa al estado de Growl
    private void CheckDistance()
    {
        if(m_stalkerBehaviour.isPlayerCatched)
            return;
        
        Vector3 dir = m_stalkerBehaviour.go_player.transform.position - transform.position;
        Debug.DrawRay(transform.position, dir * maxDistanceToGrowlState, Color.red);
        if (Physics.Raycast(m_tf_rayPoint.position, dir, out RaycastHit hit, maxDistanceToGrowlState))
        {

            if (hit.transform.gameObject.CompareTag("Player"))
            {
                growlCalled = true;
                m_stalkerBehaviour.EnterState(m_stalkerBehaviour.growlState);
            }
        }
    }
}
