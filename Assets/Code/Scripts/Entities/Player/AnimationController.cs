using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Player
{

public class AnimationController : StaticInstance<AnimationController>{

	private bool isMoving = false;

	private int WeaponMax = System.Enum.GetValues(typeof(WeaponType)).Length - 1;
	private Animator animator;
	private PlayerDataSO playerData;

	void Start(){
		animator = GetComponent<Animator>();
		playerData = PlayerController.Instance.playerData;
	}

	void Update(){
		animator.SetFloat("WeaponType", (float)playerData.CurrentWeapon/WeaponMax);
		animator.SetBool("isMoving", isMoving);
	}

	public void SetIsMoving(bool isMoving){
		this.isMoving = isMoving;
	}
}

}
