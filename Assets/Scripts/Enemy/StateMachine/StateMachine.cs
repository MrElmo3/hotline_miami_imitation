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

	public bool playerSound; //
	public bool playerView;

	void Start(){
		GameManager.Instance.EnemiesInGame++;
		EnableState(initialState);
	}

	public void EnableState(MonoBehaviour nextState){
		if (currentState != null) currentState.enabled = false;
		currentState = nextState;
		currentState.enabled = true;
	}

	private void OnDestroy(){
		GameManager.Instance.EnemiesInGame--;
	}
}
