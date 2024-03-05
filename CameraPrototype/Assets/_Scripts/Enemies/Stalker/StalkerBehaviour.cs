using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class StalkerBehaviour : MonoBehaviour
{
    [SerializeField] private Renderer objectMesh;
    [SerializeField] private GameObject player;
    private bool isPlayerNear = false;
    [SerializeField] private States state;

    public bool isStunned = false;
    public bool isVisible = false;

    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private Collider collision;
    [SerializeField] private Collider triggerCollision;

    #region Time Variables
    [Space]
    [Header("Time Variables")]
    [SerializeField] private float timeBeforeChangingPoint = 5f;
    [SerializeField] private float maxTimeStunned = 3f;
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private float stunnedTime = 3f;
    [SerializeField] private float maxTimeLooked = 2f;
    [SerializeField] private float currentTimeLooked = 0;

    #endregion

    private bool stunFinished = false;
    List<Coroutine> activesCoroutines = new();



    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        state = States.Stalk;
        transform.LookAt(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (isStunned == true)
        {
            state = States.Stunned;
        }

        if (objectMesh.isVisible)
        {
            isVisible = true;
        }
        else
        {
            // if (isVisible && state == States.Stalk)
            // {
            //     TPToNextPosition();
            // }
            isVisible = false;
        }



        switch (state)
        {
            case States.Stalk:
                Stalk();
                break;
            case States.Chase:
                Chase();
                break;
            case States.Stunned:
                Stunned();
                break;
            case States.Searching:
                Search();
                break;
            case States.PlayerCatched:
                break;
            default:
                Debug.Log("Error no state detected");
                break;
        }
    }

    private void ResetTimer()
    {
        currentTime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && currentTimeLooked >= maxTimeLooked)
        {
            Debug.Log("Player detectado");
            isPlayerNear = true;
            state = States.PlayerCatched;
            StartCoroutine(CatchPlayer(other.gameObject));
            //state = States.Chase;
        }
    }

    private IEnumerator CatchPlayer(GameObject player)
    {
        navMesh.isStopped = true;
        navMesh.velocity = Vector3.zero;
        transform.position = player.GetComponent<WatchEnemy>().enemyCatchTp.position;
        player.GetComponent<PlayerMovement>().m_canWalk = false;
        yield return null;
        //EndGame
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (currentTimeLooked >= maxTimeLooked)
            {
                state = States.Searching;
            }
            else
            {
                state = States.Stalk;
            }
        }
    }

    #region StalkBehaviour
    private void Stalk()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timeBeforeChangingPoint && !objectMesh.isVisible)
        {
            TPToNextPosition();
            ResetTimer();
        }
    }

    private void TPToNextPosition()
    {
        if (StalkPointsManager.instance.activeStalkPoints.Count <= 0)
        {
            return;
        }
        gameObject.transform.position = StalkPointsManager.instance.activeStalkPoints[Random.Range(0, StalkPointsManager.instance.activeStalkPoints.Count)].transform.position;
        transform.LookAt(player.transform);
    }

    public void AddVision(float deltaTime)
    {
        Debug.Log($"Time looked = {currentTimeLooked}");
        currentTimeLooked += deltaTime;

        if (currentTimeLooked >= maxTimeLooked)
        {
            state = States.Chase;
        }
    }
    #endregion

    #region ChaseBehaviour
    private void Chase()
    {
        navMesh.SetDestination(player.transform.position);
    }
    #endregion

    #region StunnedBehaviour
    private void Stunned()
    {
        if (stunFinished == true)
        {
            activesCoroutines.Add(StartCoroutine(ResetStunnedState()));
        }
    }

    private IEnumerator ResetStunnedState()
    {
        stunFinished = false;
        yield return new WaitForSeconds(stunnedTime);
        isStunned = false;
        if (currentTimeLooked >= maxTimeLooked)
        {
            state = States.Searching;
        }
        else
        {
            state = States.Stalk;
        }
        stunFinished = true;
    }

    private void ResetStunVariables()
    {
        stunFinished = true;
        isStunned = false;
    }
    #endregion

    #region SearchBehaviour
    private void Search()
    {

    }

    #endregion

    public void ActivateCollision()
    {
        collision.isTrigger = false;
        triggerCollision.enabled = true;
    }

}

public enum States
{
    Stalk,
    Chase,
    Stunned,
    Searching,
    PlayerCatched
}
