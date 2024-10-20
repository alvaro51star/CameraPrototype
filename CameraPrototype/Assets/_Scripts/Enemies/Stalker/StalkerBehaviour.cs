using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class StalkerBehaviour : Enemy
{
    #region Variables

    [SerializeField] private UIManager uiManager;
    [FormerlySerializedAs("objectMesh")] public Renderer rdr_objectMesh;
    [FormerlySerializedAs("player")] public GameObject go_player;
    [FormerlySerializedAs("animator")] public Animator animtr_animator;

    [FormerlySerializedAs("pointToLook")] public Transform tf_pointToLook;
    [Space]
    [Header("States")]
    public State currentState;
    public StalkState stalkState;
    public StunnedState stunnedState;
    public ChaseState chaseState;
    public PlayerCatchState playerCatchState;
    public OutOfSightState outOfSightState;
    public GrowlState growlState;

    public State lastState = null;

    [Space]
    [Header("Public variables")]
    public bool isVisible = false;
    public bool isStunned = false;
    [FormerlySerializedAs("chasePlayer")] public bool isChasingPlayer = false;
    public bool isGrowling = false;
    [Range(0.5f, 10f)]
    public float enemySpeed = 3.5f;

    [FormerlySerializedAs("navMAg_navMeshAgent")]
    [FormerlySerializedAs("navMesh")]
    [Space]
    [Header("Collision related variables")]
    [SerializeField] private NavMeshAgent m_navMAg_navMeshAgent;
    [FormerlySerializedAs("coll_collision")] [FormerlySerializedAs("collision")] [SerializeField] private Collider m_coll_collision;
    [FormerlySerializedAs("triggerCollision")] [SerializeField] private Collider m_coll_triggerCollision;
    [FormerlySerializedAs("playerCatched")] public bool isPlayerCatched = false;



    [Space]
    [Header("Time Variables")]
    public float maxTimeLooked = 2f;
    public float currentTimeLooked = 0;


    [FormerlySerializedAs("finalTpPoint")]
    [Space]
    [Header("Persecution Variables")]
    [SerializeField] private Transform m_tf_finalTpPoint;
    [FormerlySerializedAs("triggerRadius")] [SerializeField] private float m_triggerRadius = 1f;
    [FormerlySerializedAs("enemyPersecutionSpeed")] [SerializeField] private float m_enemyPersecutionSpeed = 2f;
    public bool isInPersecution = false;

    #endregion
    

    private void Awake()
    {
        m_navMAg_navMeshAgent = GetComponent<NavMeshAgent>();
        go_player = FindObjectOfType<PlayerMovement>().gameObject;
        uiManager = FindObjectOfType<UIManager>();

        //Set ups de los estados
        stalkState.SetUp(gameObject, rdr_objectMesh, animtr_animator, this, m_navMAg_navMeshAgent);
        stunnedState.SetUp(m_navMAg_navMeshAgent, animtr_animator, this);
        chaseState.SetUp(m_navMAg_navMeshAgent, go_player, animtr_animator, this);
        playerCatchState.SetUp(m_navMAg_navMeshAgent, go_player, uiManager, animtr_animator, gameObject, this);
        outOfSightState.SetUp(gameObject, this, m_navMAg_navMeshAgent);
        growlState.SetUp(gameObject, this, m_navMAg_navMeshAgent);
    }

    void Start()
    {
        if (!go_player)
        {
            Debug.LogError("No player found, please check if there is a player in scene");
        }

        if (!isPlayerCatched)
        {
            m_navMAg_navMeshAgent.speed = enemySpeed;
            currentState = stalkState;
            currentState.Enter();
        }
        

        transform.LookAt(go_player.transform);
    }


    void Update()
    {
        transform.LookAt(new Vector3(go_player.transform.position.x, transform.position.y, go_player.transform.position.z));

        if (rdr_objectMesh.isVisible)
        {
            isVisible = true;
        }
        else
        {
            isVisible = false;
        }

        if (isPlayerCatched == false)
        {
            if (currentState.isComplete)
            {
                SelectNextState();
            }
        }

        currentState.Do();
    }

    private void SelectNextState()
    {
        currentState.Exit();

        if (currentTimeLooked >= maxTimeLooked || isChasingPlayer || isInPersecution)
        {
            currentState = chaseState;
        }
        else
        {
            currentState = stalkState;
        }

        if (isPlayerCatched == true)
        {
            currentState = playerCatchState;
        }

        currentState.Enter();
    }

    //Solo sirve para cuando le estan persiguiendo que haga lo de que le pille
    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction = go_player.transform.position - tf_pointToLook.position;

        if (other.gameObject.CompareTag("Player") && currentState == chaseState)
        {
            if (Physics.Raycast(tf_pointToLook.position, direction, out RaycastHit hitInfo))
            {
                Debug.Log(hitInfo.transform.name);
                if (hitInfo.transform.CompareTag("Player") == false)
                    return;

                EnterState(playerCatchState);
                isPlayerCatched = true;
            }
        }
    }

    #region StalkBehaviour


    //Anade vision al enemigo
    public void AddVision(float deltaTime)
    {
        if (isStunned || isPlayerCatched == true || isGrowling)
        {
            go_player.GetComponent<WatchEnemy>().DesactivateFeedback();
            return;
        }

        go_player.GetComponent<WatchEnemy>().ActivateFeedbacks();

        currentTimeLooked += deltaTime;

        if (currentTimeLooked >= maxTimeLooked)
        {
            EnterState(chaseState);
        }

        EventManager.OnTimeAdded?.Invoke(currentTimeLooked, maxTimeLooked);
    }
    #endregion


    #region Useful Functions

    public void ActivateCollision()
    {
        m_coll_collision.isTrigger = false;
        m_coll_triggerCollision.enabled = true;
    }

    public void StunEnemy()
    {
        isStunned = true;
        EnterState(stunnedState);
    }

    //Funcion que se encarga de cambiar de estados
    public void EnterState(State state)
    {
        if (currentState)
        {
            currentState.Exit();
            lastState = currentState;
        }
        currentState = state;
        currentState.Enter();
    }

    public bool IsAttackingPlayer()
    {
        if (currentState == playerCatchState)
        {
            return true;
        }
        return false;
    }


    public void EventPersecution()
    {
        m_navMAg_navMeshAgent.enabled = false;
        transform.position = m_tf_finalTpPoint.position;
        m_navMAg_navMeshAgent.enabled = true;
        isInPersecution = true;
        EnterState(growlState);

        //Valores para la persecución
        (m_coll_triggerCollision as SphereCollider).radius = m_triggerRadius;
        m_navMAg_navMeshAgent.speed = m_enemyPersecutionSpeed;
    }


    #endregion


    public override void KillPlayer()
    {
        base.KillPlayer();
        isPlayerCatched = true;
        EnterState(playerCatchState);
        gameObject.GetComponent<AnomalyEnemy>().PhotoAction();
        //Activar enemigo
        
    }
}
