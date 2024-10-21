using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class GrowlState : State
{
    [FormerlySerializedAs("enemy")] [SerializeField] private GameObject m_go_enemy;
    [FormerlySerializedAs("navMeshAgent")] [SerializeField] private NavMeshAgent m_NavMAg_navMeshAgent;
    
    [FormerlySerializedAs("growlAnimation")] [SerializeField] private AnimationClip m_AnimClp_growlAnimation;

    private float m_animationLenght, m_currentTime = 0f;


    public override void Enter()
    {
        m_stalkerBehaviour.isGrowling = true;
        animtr_animator.enabled = true;
        m_animationLenght = m_AnimClp_growlAnimation.length;

        m_stateName = "Growl";
        EventManager.OnStatusChange?.Invoke(m_stateName);

        animtr_animator.Play("Growl");

        AudioManager.Instance.PlayOneShot(FMODEvents.instance.stalkerGrowling /*, this.transform.position */);

        m_NavMAg_navMeshAgent.isStopped = true;
    }

    public override void Do()
    {
        m_currentTime += Time.deltaTime;

        if (m_currentTime >= m_animationLenght)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {
        m_currentTime = 0f;
        isComplete = false;
        m_stalkerBehaviour.isGrowling = false;
        m_stalkerBehaviour.isChasingPlayer = true;
    }

    public void SetUp(GameObject enemy, StalkerBehaviour stalkerBehaviour, NavMeshAgent navMeshAgent)
    {
        this.m_go_enemy = enemy;
        this.m_stalkerBehaviour = stalkerBehaviour;
        this.m_NavMAg_navMeshAgent = navMeshAgent;
    }
}
