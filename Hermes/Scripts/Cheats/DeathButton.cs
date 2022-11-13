using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathButton : MonoBehaviour
{
    public Health[] players;

    public void Kill()
    {
        foreach (Health player in players)
        {
            if (player.gameObject.activeSelf == true)
            {
                player.TakeDamage(10000);
                player.gameObject.GetComponent<AiAgent>().stateMachine.ChangeState(AiStateID.Hit);
            }
        }
    }
}
