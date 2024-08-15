using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrowlState : State
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private NavMeshAgent navMeshAgent;
    

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AnimationClip growlAnimation;

    private float animationLenght, currentTime = 0f;


    public override void Enter()
    {
        stalkerBehaviour.isGrowling = true;
        animator.enabled = true;
        animationLenght = growlAnimation.length;

        stateName = "Growl";
        EventManager.OnStatusChange?.Invoke(stateName);

        animator.Play("Growl");

        // audioSource.Play(); //Sonido de aullido

        navMeshAgent.isStopped = true;
    }

    public override void Do()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= animationLenght)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {
        // audioSource.Stop(); //Para el sonido
        currentTime = 0f;
        isComplete = false;
        stalkerBehaviour.isGrowling = false;
        stalkerBehaviour.chasePlayer = true;
    }

    public void SetUp(GameObject enemy, StalkerBehaviour stalkerBehaviour, NavMeshAgent navMeshAgent)
    {
        this.enemy = enemy;
        this.stalkerBehaviour = stalkerBehaviour;
        this.navMeshAgent = navMeshAgent;
    }
}
