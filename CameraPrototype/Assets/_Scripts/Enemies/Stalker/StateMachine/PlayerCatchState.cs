using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCatchState : State
{
    private NavMeshAgent navMesh;
    private GameObject player;

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

    public void SetUp(NavMeshAgent navMeshAgent, GameObject player, UIManager uiManager)
    {
        this.navMesh = navMeshAgent;
        this.player = player;
        this.uiManager = uiManager;
    }

    private IEnumerator CatchPlayer(GameObject player)
    {
        navMesh.isStopped = true;
        navMesh.velocity = Vector3.zero;
        transform.position = player.GetComponent<WatchEnemy>().enemyCatchTp.position;
        animator.Play("Kill");
        player.GetComponent<PlayerMovement>().m_canWalk = false;
        yield return null;
        //EndGame
        uiManager.ActivateEndMenu();
    }
}
