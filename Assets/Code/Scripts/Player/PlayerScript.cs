using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour{

	[SerializeField] private float acelerationTime;
	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private Transform firePivot;
	[SerializeField] private bool isDead;
	
	private Vector2 currentVelocity;
	private Vector2 movementVector;
	private Vector2 inputVector;

	private Rigidbody2D rb;
	private Animator animator;

	void Start(){
		isDead = false;
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
		if(!IsAlive())
			return;
		movementVector = Vector2.SmoothDamp(movementVector, inputVector, ref currentVelocity, acelerationTime);
		rb.velocity = movementVector * speed;

		if(rb.velocity.magnitude < 0.01f){
			rb.velocity = Vector3.zero;
		}

		animator.SetBool("isMoving", inputVector.magnitude!= 0);
	}

	private void Aim(){
		if(!IsAlive())
			return;
		Vector2 mousePosition = Input.mousePosition;
		Vector2 distance = Camera.main.ScreenToWorldPoint(mousePosition) - firePivot.transform.position;

		float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
		if(distance.magnitude > 1.5f)
			transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0, 0, angle),rotateSpeed*Time.deltaTime);
	}
	
	private void  GetInput(){
		float xAxis = Input.GetAxisRaw("Horizontal");
		float yAxis = Input.GetAxisRaw("Vertical");

		inputVector = new Vector2(xAxis, yAxis);
	}

	public void Die(){
		isDead = true;
		rb.velocity = Vector2.zero;
		animator.SetBool("isDead", true);
		GameManager.instance.PlayerIsDead = true;
	}

	public bool IsAlive(){
		return !isDead;
	}
}