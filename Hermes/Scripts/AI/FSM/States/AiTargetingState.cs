using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTargetingState : State
{
    const float maxTimer = 1.0f;
    float timer;
    public static Vector3 randomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        return navHit.position;
    }
    public AiStateID GetID()
    {
        return AiStateID.Targeting;
    }

    public void Enter(AiAgent agent)
    {
        timer = 0;
        agent.animatorController.animator.SetBool("Targeting", true);
        agent.animatorController.animator.SetBool("Moving", true);
        //agent.navMeshAgent.SetDestination(randomNavSphere(agent.gameObject.transform.position, 4, 1));
        //agent.animatorController.animator.SetTrigger("Dash");
        //agent.animatorController.animator.SetInteger("DashDirection", (int)Random.Range(1, 8));
        agent.navMeshAgent.updateRotation = false;
    }

    public void Exit(AiAgent agent)
    {
        agent.animatorController.animator.SetBool("Targeting", false);
        agent.navMeshAgent.updateRotation = true;
    }

    public void UpdateState(AiAgent agent)
    {
        if (agent.targetTransform == null)
        {
            agent.stateMachine.ChangeState(AiStateID.Move);
        }
        else
        {
            timer += Time.deltaTime;
            Vector3 focusVector = agent.targetTransform.position - agent.transform.position;
            agent.transform.rotation = Quaternion.LookRotation(focusVector).normalized;
            agent.animatorController.animator.SetFloat("Velocity X", agent.navMeshAgent.velocity.x);
            agent.animatorController.animator.SetFloat("Velocity Z", agent.navMeshAgent.velocity.z);
            if (timer > maxTimer)
            {
                agent.stateMachine.ChangeState(AiStateID.ChaseTarget);
            }
        }
    }
}
