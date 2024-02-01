using System;
using Player;
using UnityEngine;
using Weapons;

public class PickUpWeapons : MonoBehaviour{

	[SerializeField] private AudioClip pickUp;

	private PlayerDataSO playerData;
	[SerializeField] private GameObject weapon;

	private void Start() {
		playerData = GetComponent<PlayerController>().playerData;
	}

	private void OnTriggerStay2D(Collider2D collision){
		if (collision.tag == "Weapon")
			weapon = collision.gameObject;
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Weapon")
			weapon = null;
	}

	public void PickUp(){
		if(weapon != null){
			WeaponDataSO weaponData = weapon.GetComponent<WeaponScript>().WeaponData;
			playerData.CurrentWeapon = weaponData;
			//AudioManager.Instance.Play(pickUp);
			Destroy(weapon);
		}
	}
}
