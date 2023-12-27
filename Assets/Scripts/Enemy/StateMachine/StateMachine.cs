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

	private PlayerScript player;
	private Animator animator;
	public bool playerSound; //
	public bool playerView;

	
	void Start(){
		animator = GetComponentInChildren<Animator>();
		player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
		try{
			GameManager.instance.EnemiesInGame++;
		}catch{
			Debug.Log("No se ha encontrado el GameManager");
		}
		EnableState(initialState);

	}

	public void EnableState(MonoBehaviour nextState){
		if (currentState != null) currentState.enabled = false;
		previousState = currentState;
		currentState = nextState;
		currentState.enabled = true;
	}

	public PlayerScript GetPlayer(){
		return player;
	}

	public MonoBehaviour GetCurrentState(){
		return currentState;
	}
	public MonoBehaviour GetPreviousState(){
		return previousState;
	}
	public Animator getAnimator(){
		return animator;
	}
}
