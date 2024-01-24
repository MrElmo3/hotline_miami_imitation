using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
	[SerializeField] List<Transform> waypoints;
	[SerializeField] private float speed = 8.0f;
	[SerializeField] private float _rotationSpeed = 180;

	private StateMachine stateMachine;

	private int waypointIndex;

	void Start(){
		stateMachine = GetComponent<StateMachine>();
		waypointIndex = 0;
	}


	private void Update(){
		if(waypoints.Count > 0){
			Move();
			RotateTowardsTarget();
		}
		if ((stateMachine.playerView || stateMachine.playerSound) && stateMachine.GetPlayer().IsAlive()){
			stateMachine.EnableState(stateMachine.alertState);
		}
	}

	private void Move(){
		stateMachine.getAnimator().SetBool("isWalking", true);
		transform.position = Vector2.MoveTowards(
			transform.position, 
			waypoints[waypointIndex].position, 
			speed * Time.deltaTime);

		if (this.transform.position == waypoints[waypointIndex].position){
			waypointIndex = (waypointIndex + 1) % waypoints.Count;
		}
	}

	private void RotateTowardsTarget(){
		Quaternion targetRotation = Quaternion.LookRotation(transform.forward, GetDirection());
		Quaternion rotation = Quaternion.RotateTowards(
			transform.rotation, targetRotation, 
			_rotationSpeed * Time.deltaTime);

		transform.rotation = rotation;
	}

	private Vector2 GetDirection(){
		Vector2 distance = waypoints[waypointIndex].position - transform.position;
		return distance.normalized;
	}
}
