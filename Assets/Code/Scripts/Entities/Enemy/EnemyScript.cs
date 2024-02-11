using UnityEngine;
using UnityEditor;
using Weapons;

namespace Enemy
{

public class EnemyScript : MonoBehaviour {
	public EntityDataSO EnemyData{ get ; private set; }

	[SerializeField] private WeaponType weapon;

	private void Start() {
		SetData();
	}

	private void OnValidate() {
		if(EditorApplication.isPlayingOrWillChangePlaymode)
			SetData();
	}

	private void SetData(){
		if(EnemyData == null)
			EnemyData = new EntityDataSO();
		EnemyData.CurrentWeapon = weapon;
	}
}

}