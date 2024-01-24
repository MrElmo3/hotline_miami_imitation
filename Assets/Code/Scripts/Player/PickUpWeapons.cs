using System;
using UnityEngine;

public class PickUpWeapons : MonoBehaviour
{
	[SerializeField] private PlayerWeapon pWeapon;
	[SerializeField] private AudioSource pickUp;
	[SerializeField] private PlayerScript player;

	private void OnTriggerStay2D(Collider2D collision){
		if (!player.IsAlive()) return;
		if (PickUp(collision, "Handgun"))
        {
			pickUp.Play();
			Destroy(collision.gameObject);
			pWeapon.WeaponType = WeaponType.Handgun;
		}
		else if (PickUp(collision, "Shotgun"))
        {
			pickUp.Play();
			Destroy(collision.gameObject);
			pWeapon.WeaponType = WeaponType.Shotgun;
		}
		else if (PickUp(collision, "Automatic"))
        {
			pickUp.Play();
			Destroy(collision.gameObject);
			pWeapon.WeaponType = WeaponType.Automatic;
		}
	}

    private bool PickUp(Collider2D collision, String tag)
    {
		return collision.transform.CompareTag(tag) && Input.GetKey(KeyCode.E);
	}
}
