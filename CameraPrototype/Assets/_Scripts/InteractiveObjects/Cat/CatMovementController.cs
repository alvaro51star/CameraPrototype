using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatMovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> destination;
    private int destinationPoint, maxDestinationPoints;
    private Collider m_collider;

    private void Start()
    {
        maxDestinationPoints = destination.Count;
        m_collider = GetComponent<Collider>();
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
                return;
            }
            agent.transform.LookAt(destination[destinationPoint - 1].position);
            agent.SetDestination(destination[destinationPoint - 1].position);
        }
        else
        {
            m_collider.enabled = false;
            this.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.gameObject.CompareTag("Player"))
            return;
        CatMovement();       
    }
}
