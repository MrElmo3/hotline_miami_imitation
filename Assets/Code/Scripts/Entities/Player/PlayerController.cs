using System.Collections;
using UnityEngine;
using Weapons;

namespace Player
{
	
public class PlayerController : StaticInstance<PlayerController> {
		
	[SerializeField] public PlayerDataSO playerData;
	[SerializeField] private Transform firePivot;

	private Vector2 input;
	private bool canAttack = true;

	private Rigidbody2D rb;		
	private void Start() {
		playerData.IsAlive = true;
		rb = GetComponent<Rigidbody2D>();
		playerData.CurrentWeapon = playerData.DefaultWeapon;
	}

	private void Update() {
		if(!playerData.IsAlive) return;
		GetInput();
		Aim();
		TryAttack();
		LeftButtonAction();
	}

	private void FixedUpdate() {
		if(!playerData.IsAlive) return;
		Move();
	}

	private void GetInput(){
		float xAxis = Input.GetAxisRaw("Horizontal");
		float yAxis = Input.GetAxisRaw("Vertical");
		input = new Vector2(xAxis, yAxis);
	}

	private void Move(){
		rb.velocity = input.normalized * playerData.Speed;
		AnimationController.Instance.SetIsMoving(input.magnitude != 0);
	}

	private void Aim(){
		Vector2 mousePosition = Input.mousePosition;
		Vector2 distance = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;

		float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
		if(distance.magnitude > 1.5f)
			transform.rotation = Quaternion.Lerp(
				transform.rotation,
				Quaternion.Euler(0, 0, angle),
				playerData.RotateSpeed * Time.deltaTime
			);
	}

	public void Die(){
		playerData.IsAlive = false;
		rb.velocity = Vector2.zero;
		//anim.SetIsAlive(false);
	}

	private void TryAttack(){
		WeaponDataSO weapon = WeaponResourceSystem.Instance.GetWeapon(playerData.CurrentWeapon);
		RaycastHit2D hitf = Physics2D.Raycast(
			transform.position, 
			transform.right, 
			Vector3.Distance(transform.position, firePivot.position), 
			LayerMask.GetMask("Walls")
		);
		if(hitf.collider != null) return;

		if(Input.GetMouseButton(0) && canAttack){
			WeaponAttack.Attack(gameObject, weapon);
			StartCoroutine(AttackCooldown(1/weapon.AttackSpeed));
		}   
	}

	private void LeftButtonAction(){
		if(Input.GetMouseButtonDown(1)){
			//trow weapon
			GetComponent<ThrowWeapons>().Throw();

			//pickup weapon
			GetComponent<PickUpWeapons>().PickUp();
		}
	}	

	public void DisableMelleHitbox(){
		transform.Find("MeleeHitbox").gameObject.SetActive(false);
	}

	private IEnumerator AttackCooldown(float time){
		canAttack = false;
		yield return new WaitForSeconds(time);
		canAttack = true;
	}
}

}