using System;
using UnityEngine;


public enum WeaponType
{
	Handgun,
	Shotgun,
	Automatic,
	Melee
}
public class Player : MonoBehaviour
{
	[SerializeField] private float acelerationTime;
	[SerializeField] private float speed;

	[SerializeField] private Transform firePivot;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float timeBetweenShots;
	[SerializeField] private float automaticFireRate;
	[SerializeField] private bool shootingInDelay;
	[SerializeField] private int dispersionAngle;
	[SerializeField] private int numBullets;
	[SerializeField] private WeaponType weaponType;
	
	private float nextTimeToShot;
	private bool isShooting;
	private float angleIncrement;
	private Vector3 adjustVectorX;
	private Vector3 adjustVectorY;
	public int ammo;

	private Vector2 currentVelocity;
	private Vector2 movementVector;
	private Vector2 inputVector;

	private Rigidbody2D rb;
	private Animator animator;

	public WeaponType WeaponType
	{
		get => weaponType;
		set
		{
			weaponType = value;
			if (value == WeaponType.Automatic)
			{
				ammo = 24;
			}
			else if (value == WeaponType.Shotgun)
			{
				ammo = 6;
			}
			else if (value == WeaponType.Handgun)
			{
				ammo = 8;
			}
		}
	}

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
		angleIncrement = dispersionAngle / numBullets*1.0f;
	}

	void Update()
	{
		Aim();
		GetInput();
		GetAttackInput();
		TryShoot();
	}

    private void FixedUpdate()
	{	
		Move();
	}
	private void Move()
	{
		movementVector = Vector2.SmoothDamp(movementVector, inputVector, ref currentVelocity, acelerationTime);
		rb.velocity = movementVector * speed;

		animator.SetBool("isMoving", inputVector.magnitude!= 0);
	}

	private void Aim()
	{
		Vector2 mousePosition = Input.mousePosition;
		Vector2 distance = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
		float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle-90);
	}

	
	private void  GetInput()
	{
		float xAxis = Input.GetAxisRaw("Horizontal");
		float yAxis = Input.GetAxisRaw("Vertical");
		inputVector = new Vector2(xAxis, yAxis);
	}

	private void GetAttackInput()
    {
		if( WeaponType != WeaponType.Automatic)
        {
			isShooting = Input.GetMouseButtonDown(0);
			if (Input.GetMouseButtonDown(0) && ammo >0)
			{
				shootingInDelay = true;
			}
		}
        else
        {
			isShooting = Input.GetMouseButton(0);
		}
    }

	private void TryShoot()
	{
		if(WeaponType == WeaponType.Handgun)
        {
			if (isShooting || shootingInDelay)
			{
				if (Time.time >= nextTimeToShot && ammo > 0)
				{
					nextTimeToShot = Time.time + timeBetweenShots;
					Instantiate(bulletPrefab, firePivot.transform.position, firePivot.transform.rotation);
					shootingInDelay = false;
					ammo--;
				}
			}
		}
		else if(WeaponType == WeaponType.Shotgun)
        {
			if (isShooting || shootingInDelay)
			{
				if (Time.time >= nextTimeToShot && ammo > 0)
				{
					nextTimeToShot = Time.time + timeBetweenShots;
					for(int i = 0; i < numBullets/2; i++)
                    {
						adjustVectorX = firePivot.transform.right * Mathf.Sin(Mathf.Deg2Rad * angleIncrement*(i+1)) * bulletPrefab.transform.localScale.y;
						adjustVectorY = firePivot.transform.up * (bulletPrefab.transform.localScale.y - Mathf.Cos(Mathf.Deg2Rad * angleIncrement * (i + 1)) * bulletPrefab.transform.localScale.y);
						Instantiate(bulletPrefab, firePivot.transform.position - adjustVectorY - adjustVectorX, firePivot.transform.rotation * Quaternion.Euler(0, 0, angleIncrement * (i + 1)));
					}
					Instantiate(bulletPrefab, firePivot.transform.position, firePivot.transform.rotation);
					for (int i = 0; i < numBullets/2; i++)
                    {
						adjustVectorX = firePivot.transform.right * Mathf.Sin(Mathf.Deg2Rad * angleIncrement*(i+1)) * bulletPrefab.transform.localScale.y;
						adjustVectorY = firePivot.transform.up * (bulletPrefab.transform.localScale.y - Mathf.Cos(Mathf.Deg2Rad * angleIncrement * (i + 1)) * bulletPrefab.transform.localScale.y);
						Instantiate(bulletPrefab, firePivot.transform.position - adjustVectorY + adjustVectorX, firePivot.transform.rotation * Quaternion.Euler(0, 0, angleIncrement * -1 *(i + 1)));
					}
					shootingInDelay = false;
					ammo--;
				}
			}
		}
		else if (WeaponType == WeaponType.Automatic)
        {
			if (isShooting)
			{
				if (Time.time >= nextTimeToShot && ammo > 0)
				{
					nextTimeToShot = Time.time + automaticFireRate;
					Instantiate(bulletPrefab, firePivot.transform.position, firePivot.transform.rotation);
					ammo--;
				}
			}
		}
	}

}