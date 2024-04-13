using System.Collections;
using UnityEngine;

public class AIIdleState : BaseState
{
    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private float idleCooldown;

    public override void Construct()
    {
        m_enemyAIStateMotor.enemyBehaviour.ResetAgentPath();
        m_enemyAIStateMotor.anim?.SetFloat(Speed, 0);
        m_enemyAIStateMotor.isIdleDone = false;
        StartCoroutine(IdleCooldown());
    }

    public override void Transition()
    {
        if (m_enemyAIStateMotor.isPlayerOnSight)
            m_enemyAIStateMotor.ChangeState(GetComponent<AIChaseState>());
        if(m_enemyAIStateMotor.isIdleDone)
            m_enemyAIStateMotor.ChangeState(GetComponent<AIPatrolState>());
    }

    public override void Destruct()
    {
        Debug.Log($"{gameObject.name} is exiting Idle State");
        m_enemyAIStateMotor.isIdleDone = true;
    }

    private IEnumerator IdleCooldown()
    {
        yield return new WaitForSeconds(idleCooldown);
        m_enemyAIStateMotor.isIdleDone = true;
    }
}
