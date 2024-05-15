using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatMovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> destination;
    private int destinationPoint, maxDestinationPoints;
    [SerializeField] private CatSounds catSounds;

    private void Start()
    {
        maxDestinationPoints = destination.Count;
    }

    private void CatMovement()
    {
       if(!agent.hasPath)
        {
            destinationPoint++;           
            if (destinationPoint <= maxDestinationPoints)
            {              
                agent.SetDestination(destination[destinationPoint - 1].position);
            }
            else
            {
                catSounds.CatGrowl();
                this.enabled = false;
            }
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player"))
            return;
        CatMovement();       
    }
}
