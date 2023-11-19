using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public PatrolState patrolState;
    public IdleState idleState;
    public MonoBehaviour alertState;
    public MonoBehaviour initialState;

    private MonoBehaviour currentState;
    void Start()
    {
        EnableState(initialState);
    }

    public void EnableState(MonoBehaviour nextState)
    {
        if (currentState != null) currentState.enabled = false;
        currentState = nextState;
        currentState.enabled = true;
    }
}
