using UnityEngine;

namespace Enemy
{

public class StunState : EnemyBaseState{

	public StunState(){
		name = EnemyState.Stun;
	}

	public override void EnterState(EnemyStateMannager enemy){
		Debug.Log("StunState");
	}

	public override void UpdateState(EnemyStateMannager enemy){

	}
}

}