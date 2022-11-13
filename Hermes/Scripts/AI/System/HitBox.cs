using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health health;
    public AiAgent agent;

    private void Start()
    {
        health = GetComponent<Health>();
        agent = GetComponent<AiAgent>();
    }
    public void OnHit(Weapon weapon)
    {
        health.TakeDamage(weapon.damage);
        agent.stateMachine.ChangeState(AiStateID.Hit);
    }
}
