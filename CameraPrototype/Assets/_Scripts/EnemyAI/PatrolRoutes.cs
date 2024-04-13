using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum PatrolRoute
{
    PathwayToRiver,
    RiverToPathWay,
    City01,
    City02
}

public class PatrolRoutes : MonoBehaviour
{
    public List<Transform> patrolRoute01;
    public List<Transform> patrolRoute02;
    public List<Transform> patrolRoute03;

    public List<Transform> GetPatrolRoute(PatrolRoute patrolRoute)
    {
        switch (patrolRoute)
        {
            case PatrolRoute.PathwayToRiver:
                return patrolRoute01;
            
            case PatrolRoute.RiverToPathWay:
                return patrolRoute02;
            
            case PatrolRoute.City01:
                return patrolRoute03;

            case PatrolRoute.City02:
                return patrolRoute01;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(patrolRoute), patrolRoute, null);
        }
    }
}
