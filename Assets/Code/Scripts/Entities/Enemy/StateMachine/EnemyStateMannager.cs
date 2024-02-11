using UnityEngine;
using UnityEditor;

namespace Enemy
{

public enum EnemyState{
	Idle,
	Patrol,
	Alert,
	Chase,
	Attack,
	Stun,
	Dead
}

public class EnemyStateMannager : MonoBehaviour {

	[SerializeField] private EnemyState startState;
	[SerializeField] private EnemyState currentState;
	private EnemyBaseState _currentState;
	private EnemyState lastState;



#region States
	IdleState idleState = new IdleState();
	PatrolState patrolState = new PatrolState();
	AlertState alertState = new AlertState();
	ChaseState chaseState = new ChaseState();
	AttackState attackState = new AttackState();
	StunState stunState = new StunState();
	DeadState deadState = new DeadState();
#endregion

	private void Start() {
		ChangeState(startState);
	}

	private void Update() {
		_currentState.UpdateState(this);
	}

	private void OnValidate() {
		if(EditorApplication.isPlayingOrWillChangePlaymode){
			if(_currentState.name != currentState)
				ChangeState(currentState);
		}
	}

	public void ChangeState(EnemyState newState) {	
		if(_currentState != null){
			if(newState == _currentState.name) return;
			lastState = _currentState.name;
		}

		switch (newState) {
			case EnemyState.Idle:
				_currentState = idleState;
				break;
			case EnemyState.Patrol:
				_currentState = patrolState;
				break;
			case EnemyState.Alert:
				_currentState = alertState;
				break;
			case EnemyState.Chase:
				_currentState = chaseState;
				break;
			case EnemyState.Attack:
				_currentState = attackState;
				break;
			case EnemyState.Stun:
				_currentState = stunState;
				break;
			case EnemyState.Dead:
				_currentState = deadState;
				break;
		}
		currentState = newState;
		_currentState.EnterState(this);
	}
}

}