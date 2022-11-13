using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHitState : State
{
    bool bHitCurrent = false;
    public AiStateID GetID()
    {
        return AiStateID.Hit;
    }
    public void Enter(AiAgent agent)
    {
        agent.hitSound.PlayOneShot(agent.hitSound.clip);
        agent.weapon.HitEffect();
        if (!bHitCurrent)
        {
            CoroutineHandler.StartCoroutine(Hit(agent));
        }
        else
        {
            bHitCurrent = false;
            if (agent.health.currentHealth > 0)
            {
                agent.animatorController.animator.SetTrigger("Damaged");
            }
        }
    }

    public void Exit(AiAgent agent)
    {
    }

    public void UpdateState(AiAgent agent)
    {
        if (agent.health.currentHealth <= 0)
        {
            agent.stateMachine.ChangeState(AiStateID.Die);
        }
        else
        {
            agent.stateMachine.ChangeState(AiStateID.ChaseTarget);
        }
    }
    IEnumerator Hit(AiAgent agent)
    {
        bHitCurrent = true;
        yield return new WaitForSeconds(0.5f);
    }
}
