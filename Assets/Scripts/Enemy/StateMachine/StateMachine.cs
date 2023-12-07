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
	private MonoBehaviour previousState;

	public bool playerSound; //
	public bool playerView;

	void Start(){
		//GameManager.Instance.EnemiesInGame++;
		EnableState(initialState);
	}

	public void EnableState(MonoBehaviour nextState){
		if (currentState != null) currentState.enabled = false;
		previousState = currentState;
		currentState = nextState;
		currentState.enabled = true;
	}

	public MonoBehaviour GetCurrentState(){
		return currentState;
	}

	public MonoBehaviour GetPreviousState(){
		return previousState;
	}

	private void OnDestroy(){
		GameManager.Instance.EnemiesInGame--;
	}
}
