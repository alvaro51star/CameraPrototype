using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StalkerBehaviour : MonoBehaviour
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
    [Header("Final TP point")]
    [SerializeField] private Transform finalTpPoint;

    #endregion


    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        uiManager = FindObjectOfType<UIManager>();

        //Set ups de los estados
        stalkState.SetUp(gameObject, objectMesh, animator, this);
        stunnedState.SetUp(navMesh, animator, this);
        chaseState.SetUp(navMesh, player, animator, this);
        playerCatchState.SetUp(navMesh, player, uiManager, animator, gameObject, this);
        outOfSightState.SetUp(gameObject, this);
        growlState.SetUp(gameObject, this);
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


        if (currentTimeLooked >= maxTimeLooked || chasePlayer)
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
            currentState.Exit();
            currentState = chaseState;
            currentState.Enter();
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

    public void EnterState(State state)
    {
        currentState.Exit();
        lastState = currentState;
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

    //TODO hacer evento de persecucion

    // public void EventPersecution()
    // {
    //     navMesh.enabled = false;
    //     transform.position = finalTpPoint.position;
    //     navMesh.enabled = true;
    //     Growl();
    // }


    #endregion


}
