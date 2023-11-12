using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
    [SerializeField] Transform[] Waypoints;
    [SerializeField] private float speed;

    private StateMachine stateMachine;
    private SpriteRenderer spriteRenderer;
    private int waypointIndex;
 
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        Move();
        //Flip();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[waypointIndex].position, speed * Time.deltaTime);

        if (this.transform.position == Waypoints[waypointIndex].position)
        {
            waypointIndex = (waypointIndex + 1) % Waypoints.Length;
        }
    }

    private void Flip()
    {
        if(transform.position.x < Waypoints[waypointIndex].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (transform.position.y < Waypoints[waypointIndex].position.y)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
    }

}
