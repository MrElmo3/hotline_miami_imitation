using UnityEngine;
using UnityEngine.EventSystems;

namespace Enemy
{

public abstract class EnemyBaseState {

	public EnemyState name;
	public abstract void EnterState(EnemyStateMannager enemy);
	public abstract void UpdateState(EnemyStateMannager enemy);

}

}