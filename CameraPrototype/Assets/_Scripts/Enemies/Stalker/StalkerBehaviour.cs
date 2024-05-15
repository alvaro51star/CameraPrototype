using UnityEngine;
using UnityEngine.AI;

public class StalkerBehaviour : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    public Renderer objectMesh;
    public GameObject player;
    [SerializeField] private Animator animator;

    public Transform pointToLook;
    [Space]
    [Header("States")]
    State states;
    public StalkState stalkState;
    public StunnedState stunnedState;
    public ChaseState chaseState;
    public PlayerCatchState playerCatchState;
    public OutOfSightState outOfSightState;

    [Space]
    [Header("Public variables")]
    public bool isVisible = false;
    public bool isStunned = false;

    [Space]
    [Header("Collision related variables")]
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private Collider collision;
    [SerializeField] private Collider triggerCollision;

    #region Time Variables
    [Space]
    [Header("Time Variables")]
    public float maxTimeLooked = 2f;
    public float currentTimeLooked = 0;

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

        if (states.isComplete)
        {
            SelectNextState();
        }

        states.Do();
    }

    private void SelectNextState()
    {
        states.Exit();

        if (currentTimeLooked >= maxTimeLooked)
        {
            states = chaseState;
        }
        else
        {
            states = stalkState;
        }

        states.Enter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && currentTimeLooked >= maxTimeLooked && !isStunned)
        {
            Debug.Log("Player detectado");
            states = playerCatchState;
            states.Enter();
        }
    }

    #region StalkBehaviour

    public void AddVision(float deltaTime)
    {
        if (isStunned)
        {
            return;
        }

        currentTimeLooked += deltaTime;

        if (currentTimeLooked >= maxTimeLooked)
        {
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
        states = stunnedState;
        states.Enter();
    }

    public void OutOfSight()
    {
        states = outOfSightState;
        states.Enter();
    }


    #endregion


}
