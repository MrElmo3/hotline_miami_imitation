using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour
{
    private StateMachine stateMachine;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    void Update()
    {
        if (stateMachine.playerView || stateMachine.playerSound)
        {
            stateMachine.EnableState(stateMachine.alertState);
        }
    }
}
