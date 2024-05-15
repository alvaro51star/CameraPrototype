using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCatchState : State
{
    private NavMeshAgent navMesh;
    private GameObject player;
    private GameObject enemy;

    [SerializeField] private AudioClip jumpScareSound;

    [SerializeField] private UIManager uiManager;

    public override void Enter()
    {
        StartCoroutine(CatchPlayer(player));
    }

    public override void Exit()
    {
        isComplete = false;
    }

    public override void Do()
    {

    }

    public void SetUp(NavMeshAgent navMeshAgent, GameObject player, UIManager uiManager, Animator animator, GameObject enemy, StalkerBehaviour stalkerBehaviour)
    {
        this.navMesh = navMeshAgent;
        this.player = player;
        this.uiManager = uiManager;
        this.animator = animator;
        this.enemy = enemy;
        this.stalkerBehaviour = stalkerBehaviour;
    }

    private IEnumerator CatchPlayer(GameObject player)
    {
        player.GetComponent<PlayerMovement>().m_canWalk = false;
        player.transform.LookAt(enemy.transform);
        navMesh.isStopped = true;
        navMesh.velocity = Vector3.zero;
        enemy.transform.position = player.GetComponent<WatchEnemy>().enemyCatchTp.position;
        AudioManager.Instance.ReproduceSound(jumpScareSound);
        animator.Play("Kill");
        yield return new WaitForSeconds(1.5f);
        //EndGame
        TestingManager.Instance.AddTime(GameFinalState.Lost);
        uiManager.ActivateLoseMenu();
    }
}
