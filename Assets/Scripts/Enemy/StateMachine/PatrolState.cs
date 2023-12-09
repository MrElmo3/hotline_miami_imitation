using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
	[SerializeField] Transform[] Waypoints;
	[SerializeField] private float speed = 8.0f;
	[SerializeField] private float _rotationSpeed = 180;

	private StateMachine stateMachine;

	private int waypointIndex;

	void Start(){
		stateMachine = GetComponent<StateMachine>();
		waypointIndex = 0;
	}


	private void Update()
	{
		if(Waypoints.Length > 0){
			Move();
			RotateTowardsTarget();
		}
		if (stateMachine.playerView || stateMachine.playerSound)
		{
			stateMachine.EnableState(stateMachine.alertState);
		}
	}

	private void Move()
	{
		transform.position = Vector2.MoveTowards(transform.position, Waypoints[waypointIndex].position, speed * Time.deltaTime);
		if (this.transform.position == Waypoints[waypointIndex].position)
		{
			waypointIndex = (waypointIndex + 1) % Waypoints.Length;
		}
	}

	private void RotateTowardsTarget()
	{
		Quaternion targetRotation = Quaternion.LookRotation(transform.forward, GetDirection());
		Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

		transform.rotation = rotation;
	}

	private Vector2 GetDirection()
	{
		Vector2 distance = Waypoints[waypointIndex].position - transform.position;
		return distance.normalized;
	}



}
