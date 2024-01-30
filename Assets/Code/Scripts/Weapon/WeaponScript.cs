using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons{
	public class WeaponScript : MonoBehaviour{
		
		[field: SerializeField] public WeaponDataSO WeaponData {get; private set;}

		[SerializeField] private float rotationSpeed;
		[SerializeField] private float velocity;
		[SerializeField] private float timeMoving;

		private void Update() {
			GetComponent<SpriteRenderer>().sprite = WeaponData.Sprite;
		}

		public void SetWeaponData(WeaponDataSO weaponData){
			WeaponData = weaponData;
		}
	}
}

