using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private IdleState idleState;
    [SerializeField] private MonoBehaviour initialState;

    private MonoBehaviour currentState;
    void Start()
    {
        EnableState(initialState);
    }

    private void EnableState(MonoBehaviour nextState)
    {
        if (currentState != null) currentState.enabled = false;
        currentState = nextState;
        currentState.enabled = true;
    }
}
