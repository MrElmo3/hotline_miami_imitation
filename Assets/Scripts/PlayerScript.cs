using UnityEngine;

public class PlayerScript : MonoBehaviour
{

	[SerializeField] private float acelerationTime;
	[SerializeField] private float speed;

	private Vector2 currentVelocity;
	private Vector2 movementVector;

	private Rigidbody2D rb;
	private Animator animator;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponentInChildren<Animator>();
	}

	void Update()
	{
		//Debug.Log(transform.localPosition);
	}
	
	private void FixedUpdate()
	{	
		Aim();
		Move(GetInput());
	}

	private void Move(Vector2 target)
	{
		movementVector = Vector2.SmoothDamp(movementVector, target, ref currentVelocity, acelerationTime);
		rb.velocity = movementVector * speed;

		animator.SetBool("isMoving", target.magnitude!= 0);
	}

	private void Aim()
	{
		Vector2 mousePosition = Input.mousePosition;

		Vector3 distance = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
		float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.Euler(0, 0, angle+90);
	}

	private Vector2  GetInput()
	{
		float xAxis = Input.GetAxisRaw("Horizontal");
		float yAxis = Input.GetAxisRaw("Vertical");

		return new Vector2(xAxis, yAxis).normalized;
	}
}