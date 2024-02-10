using Player;
using UnityEngine;
using Weapons;

public class WeaponAttack{
	public static void Attack(GameObject entity, WeaponDataSO weaponData){

		switch (weaponData.BasicWeaponType){

			case BasicWeaponType.MELEE:
				entity.GetComponent<Animator>().SetTrigger("Attack");
				entity.transform.Find("MeleeHitbox").gameObject.SetActive(true);
				break;

			case BasicWeaponType.RANGED:
				if (entity.GetComponent<PlayerController>().playerData.Ammo <=0) return;

				entity.GetComponent<Animator>().SetTrigger("Attack");
				entity.GetComponent<PlayerController>().playerData.Ammo--;
				Vector3 firePosition = entity.transform.Find("FirePivot").transform.position;

				if(weaponData.BulletsPerShot > 1){
					float step = weaponData.Dispersion / (weaponData.BulletsPerShot-1);
					for(int i = 0; i < weaponData.BulletsPerShot; i++){
						//rotation calculus
						Quaternion bulletRotation = Quaternion.Euler(
							0, 
							0, 
							entity.transform.rotation.eulerAngles.z - weaponData.Dispersion/2 + step * i
						);
						//bullet instantiation
						BulletSpawner.Instance.SpawnBullet(firePosition, bulletRotation);
					}
					break;
				}
				BulletSpawner.Instance.SpawnBullet(firePosition, entity.transform.rotation);
				break;
		}
	}
}