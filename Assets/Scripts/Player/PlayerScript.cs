using System;
using UnityEngine;

public class Player : MonoBehaviour{

	[SerializeField] private float acelerationTime;
	[SerializeField] private float speed;

	private Vector2 currentVelocity;
	private Vector2 movementVector;
	private Vector2 inputVector;

	private Rigidbody2D rb;
	private Animator animator;

	

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
	}

	void Update(){

		Aim();
		GetInput();
	}

	private void FixedUpdate(){	
		Move();
	}
	
	private void Move(){
		movementVector = Vector2.SmoothDamp(movementVector, inputVector, ref currentVelocity, acelerationTime);
		rb.velocity = movementVector * speed;

		if(rb.velocity.magnitude < 0.01f)
        {
			rb.velocity = Vector3.zero;
        }

		animator.SetBool("isMoving", inputVector.magnitude!= 0);
	}

	private void Aim(){
		Vector2 mousePosition = Input.mousePosition;
		Vector2 distance = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;

		float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0, 0, angle-90);
	}
	
	private void  GetInput(){
		float xAxis = Input.GetAxisRaw("Horizontal");
		float yAxis = Input.GetAxisRaw("Vertical");

		inputVector = new Vector2(xAxis, yAxis);
	}

}