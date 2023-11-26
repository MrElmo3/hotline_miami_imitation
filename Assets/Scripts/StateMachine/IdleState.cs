using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour
{
    private StateMachine stateMachine;
    private VisionCone visionCone;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        visionCone = GetComponentInChildren<VisionCone>();
    }

    void Update()
    {
        if (visionCone.IsSeeingPlayer)
        {
            stateMachine.EnableState(stateMachine.alertState);
        }
    }
}
