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
				break;
		}
	}


}