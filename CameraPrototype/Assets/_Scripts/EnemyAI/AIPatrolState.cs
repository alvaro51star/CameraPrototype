using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : BaseState
{
    public override void Construct()
    {
        Debug.Log($"{gameObject.name} is entering Patrol State");
        m_enemyAIStateMotor.enemyBehaviour.SetAgentPatrolSpeed();
        m_enemyAIStateMotor.enemyBehaviour.GoToNextPoint();
        
    }

    public override void Transition()
    {
        if(m_enemyAIStateMotor.isPlayerOnSight)
            m_enemyAIStateMotor.ChangeState(GetComponent<AIChaseState>());
        
        if (m_enemyAIStateMotor.enemyBehaviour.CheckNextPoint())
            m_enemyAIStateMotor.ChangeState(GetComponent<AIIdleState>());
    }

    public override void UpdateState()
    {
        if (!m_enemyAIStateMotor.enemyBehaviour.CheckNextPoint()) return;
        m_enemyAIStateMotor.enemyBehaviour.GoToNextPoint();
    }

    public override void Destruct()
    {
        Debug.Log($"{gameObject.name} is exiting Patrol State");
    }
}
