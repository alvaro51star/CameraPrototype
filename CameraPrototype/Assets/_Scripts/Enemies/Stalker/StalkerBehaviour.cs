using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
            if (isVisible && state == States.Stalk)
            {
                TPToNextPosition();
            }
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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detectado");
            isPlayerNear = true;
            state = States.Chase;
        }
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
    #endregion

    #region ChaseBehaviour
    private void Chase()
    {
        if (isPlayerNear)
        {
            navMesh.SetDestination(player.transform.position);
        }
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
}

public enum States
{
    Stalk,
    Chase,
    Stunned,
    Searching
}
