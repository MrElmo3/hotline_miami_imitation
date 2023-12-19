using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType{
	Handgun,
	Shotgun,
	Automatic,
	Melee
}

public class PlayerWeapon : MonoBehaviour{
	[SerializeField] private Transform firePivot;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private GameObject soundPrefab;
	[SerializeField] private float timeBetweenShots;
	[SerializeField] private float automaticFireRate;
	[SerializeField] private int dispersionAngle;
	[SerializeField] private int numBullets = 1;
	[SerializeField] private WeaponType weaponType;
	[SerializeField] private AmmoText ammoText;
	[SerializeField] private GameManager gameManager;
	//[SerializeField] private AudioSource shotgunShot;
	//[SerializeField] private AudioSource rifleShot;
	[SerializeField] private GameObject shotgunShot;
	[SerializeField] private GameObject rifleShot;

	private bool isShooting;
	private float nextTimeToShot;
	private float angleIncrement;
	private int ammo;

	private AudioSource pistolShot;
	
	private Animator animator;

	public int Ammo { 
		get => ammo;
		private set{
			ammo = value;
			try{
				ammoText.UpdateText(value);
			}
			catch{
				Debug.Log("No se ha encontrado el GameManager");
			}
		}
	}

	public WeaponType WeaponType {
		get => weaponType;

		set{
			weaponType = value;
			try{
				GameManager.instance.PlayerHasWeapon = true;
			}catch{
				Debug.Log("No se ha encontrado el GameManager");
			}
			if (value == WeaponType.Automatic){
				
				ammo = 24;
				ammoText.SetText(ammo, ammo);
			}               
			else if (value == WeaponType.Shotgun){
				ammo = 6;
				ammoText.SetText(ammo, ammo);
			}
			   
			else if (value == WeaponType.Handgun){
				ammo = 8; //just for testing propouses
				ammoText.SetText(ammo, ammo);
			}
		}
	}
	void Start(){
		animator = GetComponent<Animator>();
		pistolShot = GetComponent<AudioSource>();
		WeaponType = WeaponType.Handgun;
		angleIncrement = dispersionAngle / numBullets*1.0f;
	}

	void Update(){
		angleIncrement = dispersionAngle / numBullets * 1.0f;
		GetAttackInput();
		if(gameObject.GetComponent<PlayerScript>().IsAlive())
			TryShoot();	
	}

	private void GetAttackInput(){
		if (WeaponType != WeaponType.Automatic){
			isShooting = Input.GetMouseButtonDown(0);
			return;
		}
		isShooting = Input.GetMouseButton(0);
	}

	private void TryShoot(){
		Vector3 adjustmentX;
		Vector3 adjustmentY;
		firePivot.transform.GetPositionAndRotation(out Vector3 position, out Quaternion rotation);
		Vector2 scale = bulletPrefab.transform.localScale;
		float angleRad = angleIncrement * Mathf.Deg2Rad;
		float sin;
		float cos;
		Quaternion rotateAngle;

		bool canShoot = Time.time >= nextTimeToShot && ammo > 0;
		GameObject sound;
		
		if (WeaponType == WeaponType.Handgun){
			if (isShooting && canShoot){
				nextTimeToShot = Time.time + timeBetweenShots;
				Instantiate(bulletPrefab, position, rotation);
				Instantiate(soundPrefab, position, rotation);
				animator.SetTrigger("Shoot");
				pistolShot.Play();
				Ammo--;
			}
		}
		else if (WeaponType == WeaponType.Shotgun){
			if (isShooting && canShoot){
				nextTimeToShot = Time.time + timeBetweenShots;
				
				for (int i = 0; i < numBullets / 2; i++){
					sin = Mathf.Sin(angleRad * (i + 1));
					cos = Mathf.Cos(angleRad * (i + 1));
					rotateAngle = Quaternion.Euler(0, 0, angleIncrement * (i + 1));
					Debug.Log(sin);
                    adjustmentX = firePivot.transform.right * (scale.x - cos * scale.x) * -1;
                    adjustmentY = firePivot.transform.up * sin * scale.x;

					Instantiate(bulletPrefab,
                        position + adjustmentY + adjustmentX,
						rotation * rotateAngle );
				}
				animator.SetTrigger("Shoot");
				sound = Instantiate(bulletPrefab, position, rotation);
				Destroy(sound, 2f);
				Instantiate(shotgunShot, position, Quaternion.identity);
                for (int i = 0; i < numBullets / 2; i++)
                {
                    sin = Mathf.Sin(angleRad * (i + 1));
                    cos = Mathf.Cos(angleRad * (i + 1));
                    rotateAngle = Quaternion.Euler(0, 0, angleIncrement * (-i - 1));

                    adjustmentX = firePivot.transform.right * (scale.x - cos * scale.x) * -1;
                    adjustmentY = firePivot.transform.up* sin * scale.x;

                    Instantiate(bulletPrefab,
                        position - adjustmentY + adjustmentX,
                        rotation * rotateAngle);
                }
                Ammo--;
			}
		}
		else if (WeaponType == WeaponType.Automatic){
			if (isShooting && canShoot){
				nextTimeToShot = Time.time + automaticFireRate;
				animator.SetTrigger("Shoot");
				sound = Instantiate(rifleShot, position, Quaternion.identity);
				Destroy(sound, 2f);
				Instantiate(bulletPrefab, position, rotation);
				Ammo--;
			}
		}
	}
}
