using UnityEngine;

namespace Enemy
{

public class DeadState : EnemyBaseState{

	public DeadState(){
		name = EnemyState.Dead;
	}

	public override void EnterState(EnemyStateMannager enemy){
		Debug.Log("DeadState");
	}

	public override void UpdateState(EnemyStateMannager enemy){

	}
}

}