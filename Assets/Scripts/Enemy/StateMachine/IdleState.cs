using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour
{
	private StateMachine stateMachine;
	[SerializeField] private Vector3 rotation;

	void Start(){
		stateMachine = GetComponent<StateMachine>();
		rotation = transform.rotation.eulerAngles;
	}

	void Update(){
		if(transform.rotation.eulerAngles != rotation)
			transform.rotation = Quaternion.Euler(rotation);
		if (stateMachine.playerView || stateMachine.playerSound){
			stateMachine.EnableState(stateMachine.alertState);
		}
	}
}
