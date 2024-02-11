using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapons;

/// <summary>
/// One repository for all scriptable objects. 
/// Create your query methods here to keep your business logic clean.
/// </summary>
public class WeaponResourceSystem : StaticInstance<WeaponResourceSystem> {
    public List<WeaponDataSO> WeaponList { get; private set; }
    private Dictionary<WeaponType, WeaponDataSO> _WeaponDict;

    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        WeaponList = Resources.LoadAll<WeaponDataSO>("Weapons").ToList();
        _WeaponDict = WeaponList.ToDictionary(r => r.WeaponType, r => r);
    }

    public WeaponDataSO GetWeapon(WeaponType t) => _WeaponDict[t];
    public WeaponDataSO GetRandomWeapon() => WeaponList[Random.Range(0, WeaponList.Count)];
}   