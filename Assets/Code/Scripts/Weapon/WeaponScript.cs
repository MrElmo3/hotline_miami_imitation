using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons{
	public class WeaponScript : MonoBehaviour{
		
		[field: SerializeField] public WeaponDataSO WeaponData {get; private set;}
		[SerializeField] private float rotationSpeed;
		[SerializeField] private float velocity;
		[SerializeField] private float timeMoving;

		public int actualAmmo {get; set;} = -1;

		private void Update() {
			GetComponent<SpriteRenderer>().sprite = WeaponData.Sprite;
		}

		public void SetWeaponData(WeaponDataSO weaponData){
			WeaponData = weaponData;
		}

		public void StartMoving(){
			GetComponent<Collider2D>().isTrigger = false;
			GetComponent<Rigidbody2D>().velocity = transform.right * velocity;
			GetComponent<Rigidbody2D>().angularVelocity = rotationSpeed;
			StartCoroutine(TimeMoving());
		}

		private IEnumerator TimeMoving(){
			yield return new WaitForSeconds(timeMoving);
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			GetComponent<Rigidbody2D>().angularVelocity = 0;
			GetComponent<Collider2D>().isTrigger = true;
		}
	}
}

