using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StalkerBehaviour : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    public Renderer objectMesh;
    public GameObject player;
    public Animator animator;

    public Transform pointToLook;
    [Space]
    [Header("States")]
    public State states;
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

    [Space]
    [Header("Collision related variables")]
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private Collider collision;
    [SerializeField] private Collider triggerCollision;
    public bool playerCatched = false;

    #region Time Variables
    [Space]
    [Header("Time Variables")]
    public float maxTimeLooked = 2f;
    public float currentTimeLooked = 0;

    [Space]
    [Header("Final TP point")]
    [SerializeField] private Transform finalTpPoint;

    #endregion

    // Start is called before the first frame update

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();

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
        states = stalkState;
        states.Enter();

        transform.LookAt(player.transform);
    }

    // Update is called once per frame
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
            if (states.isComplete)
            {
                SelectNextState();
            }
        }

        states.Do();
    }

    private void SelectNextState()
    {
        states.Exit();


        if (currentTimeLooked >= maxTimeLooked || chasePlayer)
        {
            states = chaseState;
        }
        else
        {
            states = stalkState;
        }

        if (playerCatched == true)
        {
            states = playerCatchState;
        }

        states.Enter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && states == chaseState)
        {
            states.Exit();
            playerCatched = true;
            Debug.Log("Player detectado");
            states = playerCatchState;
            states.Enter();
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
            states.Exit();
            states = chaseState;
            states.Enter();
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
        states.Exit();
        states = stunnedState;
        states.Enter();
    }

    public void OutOfSight()
    {
        states.Exit();
        states = outOfSightState;
        states.Enter();
    }

    public void PlayerCatch()
    {
        playerCatched = true;
        states = playerCatchState;
        states.Enter();
    }

    public void Growl()
    {
        states.Exit();
        states = growlState;
        states.Enter();
    }

    public void ChasePlayer()
    {
        chasePlayer = true;
        states = chaseState;
        states.Enter();
    }

    public bool IsAttackingPlayer()
    {
        if (states == playerCatchState)
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
