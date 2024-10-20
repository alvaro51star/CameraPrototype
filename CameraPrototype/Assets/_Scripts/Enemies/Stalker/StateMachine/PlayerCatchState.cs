using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerCatchState : State
{
    private NavMeshAgent m_NavMAg_navMeshAgent;
    private GameObject m_Go_player;
    private GameObject m_Go_enemy;

    [FormerlySerializedAs("uiManager")] [SerializeField] private UIManager m_uiManager;

    public override void Enter()
    {
        animtr_animator.enabled = true;
        animtr_animator = m_stalkerBehaviour.animtr_animator;
        m_NavMAg_navMeshAgent.enabled = false;
        m_Go_player.GetComponent<PlayerMovement>().m_isCanWalk = false;
        animtr_animator.Play("Kill");
        m_Go_enemy.transform.position = m_Go_player.GetComponent<WatchEnemy>().tf_enemyCatchTp.position;

        AudioManager.Instance.PlayOneShot(FMODEvents.instance.caught /*, this.transform.position */);

        StartCoroutine(CatchPlayer(m_Go_player));
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
        this.m_NavMAg_navMeshAgent = navMeshAgent;
        this.m_Go_player = player;
        this.m_uiManager = uiManager;
        this.animtr_animator = animator;
        this.m_Go_enemy = enemy;
        this.m_stalkerBehaviour = stalkerBehaviour;
    }

    private IEnumerator CatchPlayer(GameObject player)
    {
        yield return new WaitForSeconds(1.5f);
        //EndGame
        TestingManager.Instance.AddTime(GameFinalState.Lost);
        m_uiManager.ActivateLoseMenu();
    }


}
