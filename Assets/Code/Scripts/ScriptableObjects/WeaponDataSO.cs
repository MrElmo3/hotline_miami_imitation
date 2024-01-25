using UnityEngine;

namespace Weapons{

    public enum BasicWeaponType{
        MELEE,
        RANGED,
    }
    
    [CreateAssetMenu(menuName = "Data/Weapon Data")]
    public class WeaponDataSO : ScriptableObject {
        
        [field:SerializeField] public int Ammo { get; private set; }
        [field:SerializeField] public Sprite Sprite { get; private set; }
        [field:SerializeField] public BasicWeaponType BasicWeaponType { get; private set; }
        [field:SerializeField] public WeaponsEnum WeaponType { get; private set; }
        
    }
}

