using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChaseTargetState : State
{
    float timer = 0.0f;
    public AiStateID GetID()
    {
        return AiStateID.ChaseTarget;
    }
    public void Enter(AiAgent agent)
    {
        agent.animatorController.animator.SetBool("Targeting", true);
        agent.animatorController.animator.SetBool("Moving", true);
    }
    public void Exit(AiAgent agent)
    {
        agent.animatorController.animator.SetBool("Targeting", false);
    }
    public void UpdateState(AiAgent agent)
    {

        if(!agent.enabled)
        {
            return;
        }
        timer -= Time.deltaTime;
        
        if(!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.targetTransform.position;
        }
        if (timer < 0.0f)
        {
            if (agent.targetTransform == null)
            {
                agent.navMeshAgent.ResetPath();
                agent.stateMachine.ChangeState(AiStateID.Move);
            }
            else
            {
                float direction = (agent.targetTransform.position - agent.transform.position).sqrMagnitude;
                if (direction < Mathf.Pow(agent.config.maxDistance,2))
                {
                    agent.stateMachine.ChangeState(AiStateID.Attack);
                }
                else
                {
                    if (agent.navMeshAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathPartial)
                    {
                        agent.navMeshAgent.destination = agent.targetTransform.position;
                    }
                }
            }
            timer = agent.config.maxTime;
        }
        agent.animatorController.animator.SetFloat("Velocity X", 0);
        agent.animatorController.animator.SetFloat("Velocity Z", Mathf.Abs(agent.navMeshAgent.velocity.z + agent.navMeshAgent.velocity.x));
    }
}
