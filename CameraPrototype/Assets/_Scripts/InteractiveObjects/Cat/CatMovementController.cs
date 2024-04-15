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
        EventManager.CatPetted += OnCatPetted;
    }

    private void OnDisable()
    {
        EventManager.CatPetted -= OnCatPetted;
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
            Debug.Log(timesPetted);
            if (timesPetted <= maxTimesPetted)
            {
                Debug.Log(destination[timesPetted - 1].name);
                agent.SetDestination(destination[timesPetted - 1].position);
            }
            else
            {
                this.enabled = false;
            }
        }        
    }
}
