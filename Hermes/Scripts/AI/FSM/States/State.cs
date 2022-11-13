using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateID
{
    Idle,
    ChaseTarget,
    Targeting,
    Attack,
    Hit,
    Move,
    Die,
    GetItem
}
public interface State
{
    AiStateID GetID();
    public void Enter(AiAgent agent);
    public void UpdateState(AiAgent agent);
    public void Exit(AiAgent agent);
}