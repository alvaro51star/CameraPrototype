using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StalkerBehaviour : Enemy
{
    #region Variables

    [SerializeField] private UIManager uiManager;
    public Renderer objectMesh;
    public GameObject player;
    public Animator animator;

    public Transform pointToLook;
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
    public bool chasePlayer = false;
    public bool isGrowling = false;
    public float enemySpeed = 3.5f;

    [Space]
    [Header("Collision related variables")]
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private Collider collision;
    [SerializeField] private Collider triggerCollision;
    public bool playerCatched = false;



    [Space]
    [Header("Time Variables")]
    public float maxTimeLooked = 2f;
    public float currentTimeLooked = 0;


    [Space]
    [Header("Persecution Variables")]
    [SerializeField] private Transform finalTpPoint;
    [SerializeField] private float triggerRadius = 1f;
    [SerializeField] private float enemyPersecutionSpeed = 2f;
    public bool isInPersecution = false;

    #endregion
    

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        uiManager = FindObjectOfType<UIManager>();

        //Set ups de los estados
        stalkState.SetUp(gameObject, objectMesh, animator, this, navMesh);
        stunnedState.SetUp(navMesh, animator, this);
        chaseState.SetUp(navMesh, player, animator, this);
        playerCatchState.SetUp(navMesh, player, uiManager, animator, gameObject, this);
        outOfSightState.SetUp(gameObject, this, navMesh);
        growlState.SetUp(gameObject, this, navMesh);
    }

    void Start()
    {
        if (!player)
        {
            Debug.LogError("No player found, please check if there is a player in scene");
        }

        navMesh.speed = enemySpeed;
        currentState = stalkState;
        currentState.Enter();

        transform.LookAt(player.transform);
    }


    void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (objectMesh.isVisible)
        {
            isVisible = true;
        }
        else
        {
            isVisible = false;
        }

        if (playerCatched == false)
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

        if (currentTimeLooked >= maxTimeLooked || chasePlayer || isInPersecution)
        {
            currentState = chaseState;
        }
        else
        {
            currentState = stalkState;
        }

        if (playerCatched == true)
        {
            currentState = playerCatchState;
        }

        currentState.Enter();
    }

    //Solo sirve para cuando le estan persiguiendo que haga lo de que le pille
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && currentState == chaseState)
        {
            currentState.Exit();
            playerCatched = true;
            currentState = playerCatchState;
            currentState.Enter();
        }
    }

    #region StalkBehaviour


    //Anade vision al enemigo
    public void AddVision(float deltaTime)
    {
        if (isStunned || playerCatched == true || isGrowling)
        {
            player.GetComponent<WatchEnemy>().DesactivateFeedback();
            return;
        }

        player.GetComponent<WatchEnemy>().ActivateFeedbacks();

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
        collision.isTrigger = false;
        triggerCollision.enabled = true;
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
        navMesh.enabled = false;
        transform.position = finalTpPoint.position;
        navMesh.enabled = true;
        isInPersecution = true;
        EnterState(growlState);

        //Valores para la persecución
        (triggerCollision as SphereCollider).radius = triggerRadius;
        navMesh.speed = enemyPersecutionSpeed;
    }


    #endregion


    public override void KillPlayer()
    {
        base.KillPlayer();
        EnterState(playerCatchState);
    }
}
