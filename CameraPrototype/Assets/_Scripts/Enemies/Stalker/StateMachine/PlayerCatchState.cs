using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCatchState : State
{
    private NavMeshAgent navMesh;
    private GameObject player;
    private GameObject enemy;

    UIManager uiManager;

    public override void Enter()
    {
        StartCoroutine(CatchPlayer(player));
    }

    public override void Exit()
    {

    }

    public override void Do()
    {

    }

    public void SetUp(NavMeshAgent navMeshAgent, GameObject player, UIManager uiManager, Animator animator, GameObject enemy)
    {
        this.navMesh = navMeshAgent;
        this.player = player;
        this.uiManager = uiManager;
        this.animator = animator;
        this.enemy = enemy;
    }

    private IEnumerator CatchPlayer(GameObject player)
    {
        navMesh.isStopped = true;
        navMesh.velocity = Vector3.zero;
        transform.position = player.GetComponent<WatchEnemy>().enemyCatchTp.position;
        animator.Play("Kill");
        player.GetComponent<PlayerMovement>().m_canWalk = false;
        yield return new WaitForSeconds(1.5f);
        //EndGame
        GameManager.Instance.CopyTimeToClipboard();
        uiManager.ActivateEndMenu();
    }
}
