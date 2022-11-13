using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDieState : State
{
    public AiStateID GetID()
    {
       return AiStateID.Die;
    }
    public void Enter(AiAgent agent)
    {
        if(agent.gameObject.CompareTag("Player"))
        {
            int charcterNum = agent.gameObject.GetComponent<CharacterSystem>().slot;
            Managers.Data.Ingame.setEquippedItemWhenDead(charcterNum);
        }
        agent.animatorController.animator.SetTrigger("Die");
        agent.health.enabled = false;
        agent.navMeshAgent.isStopped = true;
        if (agent.dropTable != null)
        {
            agent.dropTable.Drop();
        }
    }

    public void Exit(AiAgent agent)
    {
    }



    public void UpdateState(AiAgent agent)
    {
    }
}
