using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiGetItem : State
{
    public AiStateID GetID()
    {
        return AiStateID.GetItem;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.ResetPath();
        agent.navMeshAgent.SetDestination(agent.dropTransform.position);
        agent.animatorController.animator.SetBool("Moving", true);

    }

    public void Exit(AiAgent agent)
    {
    }

    public void UpdateState(AiAgent agent)
    {
       if(agent.navMeshAgent.remainingDistance <= 5)
        {
            agent.stateMachine.ReturnToPreviousState();
        }
    }
}
