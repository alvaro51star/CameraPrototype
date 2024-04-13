using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAIStateMotor))]
public abstract class BaseState : MonoBehaviour
{
    //protected StateMachine m_sm;
    [SerializeField] protected EnemyAIStateMotor m_enemyAIStateMotor;

    private void Awake()
    {
        m_enemyAIStateMotor = GetComponent<EnemyAIStateMotor>();
    }

    public virtual void Construct() { }

    public virtual void Destruct() { }

    public virtual void Transition() { }

    public virtual void UpdateState() { }
    
    // public virtual void HitByPlayer(FPSPlayer player)
    // {
    //     Debug.Log("Player hit by enemy");
    //     m_enemyAIStateMotor.player = player;
    //     m_enemyAIStateMotor.isPlayerOnSight = true;
    // }

    public void ExitedByPlayer()
    {
        Debug.Log("Player exits enemy");
        m_enemyAIStateMotor.isPlayerOnSight = false;
    }
}
