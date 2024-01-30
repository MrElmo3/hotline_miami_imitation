using TMPro;
using UnityEngine;
using Weapons;

public class WeaponAttack{
	public static void Attack(GameObject entity, WeaponDataSO weaponData){
		entity.GetComponent<Animator>().SetTrigger("Attack");

		switch (weaponData.BasicWeaponType){

			case BasicWeaponType.MELEE:
				entity.transform.Find("MeleeHitbox").gameObject.SetActive(true);
				break;

			case BasicWeaponType.RANGED:

				GameObject bullet = GameObject.Find("BaseBullet");

				if(weaponData.BulletsPerShot == 1){
					_ = GameObject.Instantiate(
						bullet,
						entity.transform.Find("FirePivot").transform.position,
						entity.transform.rotation
					).GetComponent<Bullet>().enabled = true;
				}
				else{
					float step = weaponData.Dispersion / (weaponData.BulletsPerShot-1);
					for(int i = 0; i < weaponData.BulletsPerShot; i++){
						//rotation calculus
						Quaternion bulletRotation = Quaternion.Euler(
							0, 
							0, 
							entity.transform.rotation.eulerAngles.z - weaponData.Dispersion/2 + step * i
						);
						//bullet instantiation
						_ = GameObject.Instantiate(
							bullet,
							entity.transform.Find("FirePivot").transform.position,
							bulletRotation
						).GetComponent<Bullet>().enabled = true;
					}
				}
				break;
		}
	}
}