using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(AIPatrolState),typeof(AIIdleState),typeof(AIChaseState))]
[RequireComponent(typeof(EnemyAIBehaviour),typeof(Animator))]
public class EnemyAIStateMotor : MonoBehaviour
{
    public Animator anim;
    public EnemyAIBehaviour enemyBehaviour;
    //public FPSPlayer player;
    public bool isPlayerOnSight, isIdleDone;
    

    private BaseState m_state;

    private void Awake()
    {
        enemyBehaviour = GetComponent<EnemyAIBehaviour>();
        anim = GetComponent<Animator>();
        m_state = GetComponent<AIIdleState>();
    }

    private void Start()
    {
        m_state.Construct();
    }

    private void Update()
    {
        //if(!GameManager.Instance.isPaused)
        UpdateMotor();
    }

    private void UpdateMotor()
    {
        m_state.Transition();
        m_state.UpdateState();
    }

    public void ChangeState(BaseState newState)
    {
        m_state.Destruct();
        m_state = newState;
        m_state.Construct();
    }
}
