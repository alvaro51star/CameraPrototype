using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseState : BaseState
{
    public override void Construct()
    {
        m_enemyAIStateMotor.enemyBehaviour.SetAgentChaseSpeed();
        m_enemyAIStateMotor.enemyBehaviour.SetAgentTarget(m_enemyAIStateMotor.player.transform);
    }

    public override void Transition()
    {
        if (m_enemyAIStateMotor.isPlayerOnSight) return;
        m_enemyAIStateMotor.ChangeState(GetComponent<AIPatrolState>());
    }

    public override void UpdateState()
    {
        m_enemyAIStateMotor.enemyBehaviour.SetMovingDestination();
    }
}
