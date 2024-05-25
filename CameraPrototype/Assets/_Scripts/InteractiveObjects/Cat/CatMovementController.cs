using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatMovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> destination;
    private int destinationPoint, maxDestinationPoints;
    //private Collider m_collider;

    private void OnEnable()
    {
        EventManager.OnEnemyRevealed += OnEnemyRevealed;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyRevealed -= OnEnemyRevealed;
    }

    private void Start()
    {
        maxDestinationPoints = destination.Count;
    }

    private void OnDoorOpened()
    {        
        CatMovement();
    }

    private void OnEnemyRevealed()
    {
        CatMovement();
    }

    private void CatMovement()
    {
        if (agent.hasPath)
            return;

        destinationPoint++;        

        if (destinationPoint <= maxDestinationPoints)
        {
            var path = new NavMeshPath();
            agent.CalculatePath(destination[destinationPoint - 1].position, path);

            if (path.status != NavMeshPathStatus.PathComplete)
            {
                destinationPoint--;
                StartCoroutine(TryAgain());
                return;
            }
            agent.transform.LookAt(destination[destinationPoint - 1].position);
            agent.SetDestination(destination[destinationPoint - 1].position);
        }
        else
        {
            this.enabled = false;
        }
    }

    private IEnumerator TryAgain()
    {
        yield return new WaitForSeconds(3f);

        CatMovement();

    }
    
}
