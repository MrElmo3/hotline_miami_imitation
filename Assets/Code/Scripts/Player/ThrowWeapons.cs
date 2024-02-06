using Player;
using UnityEngine;
using Weapons;

public class ThrowWeapons : MonoBehaviour {
	
	[SerializeField] private GameObject weaponPrefab;
	[SerializeField] private float offsetMagnitude = 3f;
	//[SerializeField] private AudioClip throwWeapon;
	
	private PlayerDataSO playerData;
	private GameObject WeaponContainer;
	
	private void Start() {
		playerData = GetComponent<PlayerController>().playerData;
		WeaponContainer = GameObject.Find("WeaponContainer");
	}
	
	public void Throw(){
		Vector2 position = transform.position + transform.right * offsetMagnitude;
		if(playerData.CurrentWeapon.WeaponType != WeaponsEnum.UNARMED){

			GameObject throwedWeapon = Instantiate(weaponPrefab, position, transform.rotation, WeaponContainer.transform);
			throwedWeapon.GetComponent<WeaponScript>().SetWeaponData(playerData.CurrentWeapon);

			throwedWeapon.GetComponent<WeaponScript>().StartMoving();
			
			playerData.CurrentWeapon = playerData.DefaultWeapon;
			//AudioManager.Instance.Play(throwWeapon);
		}
	}
}