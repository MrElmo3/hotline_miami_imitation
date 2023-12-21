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
	public bool playerSound; //
	public bool playerView;

	[SerializeField] private GameObject hitSound;
	void Start(){
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

	private void OnDestroy(){
		try{
			GameManager.instance.EnemiesInGame--;
		}catch{
			Debug.Log("No se ha encontrado el GameManager");
		}
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.transform.CompareTag("Bullet"))
		{
			GameObject hit = Instantiate(hitSound, player.transform.position, Quaternion.identity);
			Destroy(hit, 2f);
		}
	}
}
