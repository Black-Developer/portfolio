using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMoveState : State
{
    public AiStateID GetID()
    {
        return AiStateID.Move;
    }

    public void Enter(AiAgent agent)
    {
        agent.animatorController.animator.SetBool("Moving", true);
    }
    public void Exit(AiAgent agent)
    {
        agent.animatorController.animator.SetBool("Moving", false);
    }
    public void UpdateState(AiAgent agent)
    {
        if (agent.targetTransform != null)
        {
            agent.stateMachine.ChangeState(AiStateID.ChaseTarget);
        }
        else if (agent.targetTransform == null && agent.dropTransform != null)
        {
            agent.stateMachine.ChangeState(AiStateID.GetItem);
        }
        else
        {
            agent.characterMovement.Move();
            agent.animatorController.animator.SetFloat("Velocity X", 0);//agent.navMeshAgent.velocity.normalized.x);
            agent.animatorController.animator.SetFloat("Velocity Z", (Mathf.Abs((agent.navMeshAgent.velocity.z + agent.navMeshAgent.velocity.x)) * 2));
        }
    }
}
