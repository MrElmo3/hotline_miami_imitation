using UnityEngine;
using Weapons;

public class EntityDataSO : ScriptableObject {
	[field: SerializeField] public bool IsAlive { get;  set; }
	[field: SerializeField] public WeaponDataSO CurrentWeapon { get; set; }
}