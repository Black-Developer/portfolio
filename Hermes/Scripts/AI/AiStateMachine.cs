using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine
{
    public State[] states;
    public AiAgent agent;
    public AiStateID currentState;
    public AiStateID previousState;
    public AiStateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numState = System.Enum.GetNames(typeof(AiStateID)).Length;
        states = new State[numState];
    }

    public void RegisterState(State state)
    {
        int index = (int)state.GetID();
        states[index] = state;
    }

    public State GetState(AiStateID stateID)
    {
        int index = (int)stateID;
        return states[index];
    }

    public void Update()
    {
        Debug.Log(this.agent.gameObject.name + currentState);
        GetState(currentState).UpdateState(agent);
    }
    
    public void ChangeState(AiStateID newState)
    {
        GetState(currentState).Exit(agent);
        previousState = currentState;
        currentState = newState;
        GetState(currentState).Enter(agent);

    }
    public void ReturnToPreviousState()
    {
        ChangeState(previousState);
    }
}
