using System;
using Player;
using UnityEngine;
using Weapons;

public class PickUpWeapons : MonoBehaviour{

	[SerializeField] private AudioClip pickUp;

	private PlayerDataSO playerData;

	private void Start() {
		playerData = GetComponent<PlayerController>().playerData;
	}

	private void OnTriggerStay2D(Collider2D collision){
		if (!playerData.IsAlive) return;
		if (collision.tag == "Weapon" && Input.GetKey(KeyCode.E)){
			//pickUp.Play();
			//Destroy(collision.gameObject);
			playerData.CurrentWeapon = collision.GetComponent<WeaponScript>().WeaponData;
		}
	}
}
