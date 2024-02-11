using UnityEngine;

namespace Weapons{

	public enum BasicWeaponType{
		MELEE,
		RANGED,
	}
	
	[CreateAssetMenu(menuName = "Data/Weapon Data")]
	public class WeaponDataSO : ScriptableObject {
		[field:SerializeField] public Sprite Sprite { get; private set; }
		[field:SerializeField] public BasicWeaponType BasicWeaponType { get; private set; }
		[field:SerializeField] public WeaponType WeaponType { get; private set; }
		[field:SerializeField] public int Ammo { get; set; }
		[field:SerializeField] public int BulletsPerShot { get; private set; }
		[field:SerializeField] public float Dispersion { get; private set; }//only for multiple bullets per shot weapons
		[field:SerializeField] public float AttackSpeed { get; private set; }

	}
}

