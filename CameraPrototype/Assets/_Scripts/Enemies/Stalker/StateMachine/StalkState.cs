using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkState : State
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Renderer objectMesh;

    public float currentTime;
    [SerializeField] private float timeBeforeChangingPoint = 5f;

    public override void Enter()
    {
        stateName = "Stalk";
        EventManager.OnStatusChange?.Invoke(stateName);

        currentTime = 0f;
        animator.Play("Idle");
        isComplete = false;
    }

    public override void Exit()
    {
        isComplete = false;
    }

    public override void Do()
    {
        Stalk();
    }

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
        enemy.transform.position = StalkPointsManager.instance.activeStalkPoints[Random.Range(0, StalkPointsManager.instance.activeStalkPoints.Count)].transform.position;
        //transform.LookAt(player.transform);
    }

    private void ResetTimer()
    {
        currentTime = 0;
    }

    public void SetUp(GameObject enemy, Renderer objectMesh, Animator animator, StalkerBehaviour stalkerBehaviour)
    {
        this.enemy = enemy;
        this.objectMesh = objectMesh;
        this.animator = animator;
        this.stalkerBehaviour = stalkerBehaviour;
    }
}
