using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
    [SerializeField] Transform[] Waypoints;
    [SerializeField] private float speed;

    private StateMachine stateMachine;
    private SpriteRenderer spriteRenderer;
    private VisionCone visionCone;

    private int waypointIndex;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        visionCone = GetComponentInChildren<VisionCone>();
    }


    private void Update()
    {
        Move();
        if (visionCone.IsSeeingPlayer)
        {
            stateMachine.EnableState(stateMachine.alertState);
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[waypointIndex].position, speed * Time.deltaTime);
        if (this.transform.position == Waypoints[waypointIndex].position)
        {
            waypointIndex = (waypointIndex + 1) % Waypoints.Length;
        }
    }



}
