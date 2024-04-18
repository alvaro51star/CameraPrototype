using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatMovementController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> destination;
    private int timesPetted, maxTimesPetted;

    private void OnEnable()
    {
        EventManager.OnCatPetted += OnCatPetted;
    }

    private void OnDisable()
    {
        EventManager.OnCatPetted -= OnCatPetted;
    }
    private void Start()
    {
        maxTimesPetted = destination.Count;
    }

    private void OnCatPetted()
    {
       if(!agent.hasPath)
        {
            timesPetted++;           
            if (timesPetted <= maxTimesPetted)
            {              
                agent.SetDestination(destination[timesPetted - 1].position);
            }
            else
            {
                this.enabled = false;
                this.gameObject.GetComponent<InteractiveObject>().enabled = false;
            }
        }        
    }
}
