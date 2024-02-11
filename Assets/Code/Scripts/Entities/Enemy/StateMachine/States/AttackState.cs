using UnityEngine;

namespace Enemy
{

public class AttackState : EnemyBaseState{
	
	public AttackState(){
		name = EnemyState.Attack;
	}
	
	public override void EnterState(EnemyStateMannager enemy){
		Debug.Log("AttackState");
	}

	public override void UpdateState(EnemyStateMannager enemy){
		
	}
}

}