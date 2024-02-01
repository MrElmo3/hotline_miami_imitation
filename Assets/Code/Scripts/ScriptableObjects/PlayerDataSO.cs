using UnityEngine;
using Weapons;
namespace Player{
	[CreateAssetMenu(menuName = "Data/Player Data")]
	public class PlayerDataSO : EntityDataSO {
		
		[field: SerializeField] public float Speed { get; private set; }
		[field: SerializeField] public float RotateSpeed { get; private set; }
		[field: SerializeField] public int Ammo { get; set; }
		[field: SerializeField] public WeaponDataSO DefaultWeapon { get; private set; }
		
	}
}
