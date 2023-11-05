using System;
using UnityEngine;

public class Player : MonoBehaviour
{

	[SerializeField] private float acelerationTime;
	[SerializeField] private float speed;

	[SerializeField] private Transform firePivot;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float timeBetweenShots;
	[SerializeField] private bool shootingInDelay;
	private float nextTimeToShot;
	private bool isShooting;

	private Vector2 currentVelocity;
	private Vector2 movementVector;
	private Vector2 inputVector;

	private Rigidbody2D rb;
	private Animator animator;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
	}

	void Update()
	{
		Aim();
		GetInput();
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
		isShooting = Input.GetMouseButtonDown(0);
        if (Input.GetMouseButtonDown(0))
        {
			shootingInDelay = true;
        }
		
	}

	private void TryShoot()
	{
		if (isShooting || shootingInDelay)
		{
			if(Time.time >= nextTimeToShot)
            {
				nextTimeToShot = Time.time + timeBetweenShots;
				Instantiate(bulletPrefab, firePivot.transform.position, firePivot.transform.rotation);
				shootingInDelay = false;
            }
		}
	}

}