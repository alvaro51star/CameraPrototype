using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatMovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> destination;
    private int destinationPoint, maxDestinationPoints;
    private bool firstDoor = true;
    //private Collider m_collider;

    private void OnEnable()
    {
        EventManager.OnEnemyRevealed += OnEnemyRevealed;
        EventManager.OnDoorOpened += OnDoorOpened;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyRevealed -= OnEnemyRevealed;
        EventManager.OnDoorOpened -= OnDoorOpened;
    }

    private void Start()
    {
        maxDestinationPoints = destination.Count;
    }

    private void OnDoorOpened()
    {
        if (firstDoor)
        {
            CatMovement(destination[0].position);
            firstDoor = false;
        }
        else
        {
            CatMovement(destination[2].position);
        }
    }

    private void OnEnemyRevealed()
    {
        CatMovement(destination[1].position);
    }

    private void CatMovement(Vector3 destination)
    {
        /*if (agent.hasPath)
        {
            StartCoroutine(TryAgain(destination));
            return;
        }    */    

        if (destinationPoint <= maxDestinationPoints)
        {
            var path = new NavMeshPath();
            agent.CalculatePath(destination, path);

            if (path.status != NavMeshPathStatus.PathComplete)
            {
                destinationPoint--;
                StartCoroutine(TryAgain(destination));
                return;
            }
            agent.transform.LookAt(destination);
            agent.SetDestination(destination);
        }
        else
        {
            this.enabled = false;
        }
    }

    private IEnumerator TryAgain(Vector3 destination)
    {
        yield return new WaitForSeconds(1f);

        CatMovement(destination);

    }
    
}
