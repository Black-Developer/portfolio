using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackState : State
{
    private bool b_isAttack = false;
    float weaponDelay;
    public AiStateID GetID()
    {
        return AiStateID.Attack;
    }
    public void Enter(AiAgent agent)
    {
        agent.animatorController.animator.SetBool("Targeting", true);
    }

    public void Exit(AiAgent agent)
    {
        agent.animatorController.animator.SetBool("Targeting", false);
    }

    public void UpdateState(AiAgent agent)
    {
        if (agent.targetTransform == null)
        {
            agent.stateMachine.ChangeState(AiStateID.Move);
        }
        weaponDelay += Time.deltaTime;
        if (weaponDelay > agent.weapon.attackSpeed)
        {
            b_isAttack = false;
            weaponDelay = 0;
        }
        else
        {
            if (agent.targetTransform != null)
            {
                agent.navMeshAgent.SetDestination(agent.targetTransform.position);
                // 공격범위 내부에 적이 있음
                if (agent.navMeshAgent.remainingDistance < agent.weapon.Range)
                {
                    agent.navMeshAgent.isStopped = true;

                    if (!b_isAttack)
                    {
                        CoroutineHandler.StartCoroutine(Attack(agent));
                        b_isAttack = true;
                    }
                }
                else
                {
                    agent.stateMachine.ChangeState(AiStateID.ChaseTarget);
                }
            }
        }
    }
    IEnumerator Attack(AiAgent agent)
    {
        if (agent.navMeshAgent.remainingDistance >agent.weapon.Range)
        {
            agent.navMeshAgent.isStopped = false;
            agent.stateMachine.ChangeState(AiStateID.ChaseTarget);
        }
        else
        {
            agent.animatorController.animator.SetTrigger("Attack");
            agent.weapon.weaponSound.PlayOneShot(agent.weapon.weaponSound.clip);
            agent.stateMachine.ChangeState(AiStateID.Targeting);
            agent.navMeshAgent.isStopped = false;
        }
            yield return null;
    }
}
