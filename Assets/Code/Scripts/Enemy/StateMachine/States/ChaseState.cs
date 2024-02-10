using UnityEngine;

namespace Enemy
{

public class ChaseState : EnemyBaseState{
	
	public ChaseState(){
		name = EnemyState.Chase;
	}
	
	public override void EnterState(EnemyStateMannager enemy){
		Debug.Log("ChaseState");
	}
	
	public override void UpdateState(EnemyStateMannager enemy){
		
	}
}

}