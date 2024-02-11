using UnityEngine;
using Weapons;

namespace Enemy
{

public class AnimationController : MonoBehaviour {
	
	private int WeaponMax = System.Enum.GetValues(typeof(WeaponType)).Length - 1;
	private bool isMoving = false;

	private EntityDataSO enemyData;
	private Animator animator;

	void Start(){
		animator = GetComponent<Animator>();
		enemyData = GetComponent<EnemyScript>().EnemyData;
	}

	void Update(){
		animator.SetFloat("WeaponType", (float)enemyData.CurrentWeapon/WeaponMax);
		animator.SetBool("isMoving", isMoving);
	}

	public void SetIsMoving(bool isMoving){
		this.isMoving = isMoving;
	}
}

}