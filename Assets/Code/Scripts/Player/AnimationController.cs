using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Player{

	public class AnimationController : MonoBehaviour{

		[SerializeField] private bool isMoving = false;

		private PlayerDataSO playerData;
		private int WeaponMax = 3;
		private Animator animator;

		void Start(){
			animator = GetComponent<Animator>();
			playerData = GetComponent<PlayerController>().playerData;
		}

		void Update(){
			animator.SetFloat("WeaponType", (float)playerData.CurrentWeapon.WeaponType/WeaponMax);
			animator.SetBool("isMoving", isMoving);
		}

		public void SetIsMoving(bool isMoving){
			this.isMoving = isMoving;
		}
	}
}
