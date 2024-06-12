using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrowlState : State
{
    [SerializeField] private GameObject enemy;

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

        audioSource.Play();

        enemy.GetComponent<NavMeshAgent>().isStopped = true;
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
        audioSource.Stop();
        currentTime = 0f;
        isComplete = false;
        stalkerBehaviour.isGrowling = false;
        stalkerBehaviour.ChasePlayer();
    }

    public void SetUp(GameObject enemy, StalkerBehaviour stalkerBehaviour)
    {
        this.enemy = enemy;
        this.stalkerBehaviour = stalkerBehaviour;
    }
}
