using System;
using UnityEngine;

public class PickUpWeapons : MonoBehaviour
{
	[SerializeField] private PlayerWeapon pWeapon;

	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.transform.CompareTag("Handgun"))
			pWeapon.WeaponType = WeaponType.Handgun;

		else if (collision.transform.CompareTag("Shotgun"))
			pWeapon.WeaponType = WeaponType.Shotgun;
			
		else if (collision.transform.CompareTag("Automatic"))
			pWeapon.WeaponType = WeaponType.Automatic;
	}
}
