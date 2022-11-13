using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : State
{
    public AiStateID GetID()
    {
        return AiStateID.Idle;
    }
    public void Enter(AiAgent agent)
    {
        if (agent.gameObject.GetComponent<AiAnimatorController>().animator != null)
        {
            agent.animatorController.animator.SetBool("Targeting", false);
            agent.animatorController.animator.SetBool("Moving", false);
        }
    }

    public void Exit(AiAgent agent)
    {
    }

    public void UpdateState(AiAgent agent)
    {
        // 타겟이 없는 경우
        if (agent.targetTransform == null)
        {
            if (agent.characterMovement.m_Waypoints.Count != 0)
            {
                agent.stateMachine.ChangeState(AiStateID.Move);
            }
        }
        else
        {
            Vector3 targetDirection = agent.targetTransform.position - agent.transform.position;

            if (targetDirection.sqrMagnitude > Mathf.Pow(agent.config.maxDistance, 2))
            {
                return;
            }
            agent.stateMachine.ChangeState(AiStateID.ChaseTarget);

            //Vector3 targetDirection = agent.targetTransform.position - agent.transform.position;
            //if (targetDirection.sqrMagnitude < Mathf.Pow(agent.config.maxSightDistance,2))
            //{
            //    return;
            //}
            //Vector3 agentDirection = agent.transform.forward;    
            //targetDirection.Normalize();            
            //float dotProduct = Vector3.Dot(targetDirection, agentDirection);
            //if (dotProduct > 0.0f)
            //{  
            //    agent.stateMachine.ChangeState(AiStateID.ChaseTarget); 
            //}

        }
    }
}
