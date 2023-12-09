using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IdleState : MonoBehaviour
{
	private StateMachine stateMachine;
	[SerializeField] private Vector3 rotation;
	[SerializeField] private float _rotationSpeed = 180;

	void Start(){
		stateMachine = GetComponent<StateMachine>();
		rotation = transform.rotation.eulerAngles;
	}

	void Update(){
		if(transform.rotation.eulerAngles != rotation)
			RotateTowards(rotation);
		if (stateMachine.playerView || stateMachine.playerSound){
			stateMachine.EnableState(stateMachine.alertState);
		}
	}

	private void RotateTowards(Vector3 target){
		Vector2 distance = target - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(transform.forward, distance);
		Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
		transform.rotation = rotation;
	}
}
