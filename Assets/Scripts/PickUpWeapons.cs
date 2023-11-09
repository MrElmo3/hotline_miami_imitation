using System;
using UnityEngine;

public class PickUpWeapons : MonoBehaviour
{
	[SerializeField] private Player player;

	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.transform.CompareTag("Handgun"))
			player.WeaponType = WeaponType.Handgun;

		else if (collision.transform.CompareTag("Shotgun"))
			player.WeaponType = WeaponType.Shotgun;
			
		else if (collision.transform.CompareTag("Automatic"))
			player.WeaponType = WeaponType.Automatic;
	}
}
