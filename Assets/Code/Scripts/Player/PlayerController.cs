using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Player{
	
	public class PlayerController : MonoBehaviour {
		
		[SerializeField] public PlayerDataSO playerData;
		[SerializeField] private Transform firePivot;

		private Vector2 input;
		private bool canAttack = true;

		private Rigidbody2D rb;
		private AnimationController anim;
		
		private void Start() {
			playerData.IsAlive = true;
			rb = GetComponent<Rigidbody2D>();
			anim = GetComponent<AnimationController>();
		}

		private void Update() {
			if(playerData.IsAlive){
				GetInput();
				Aim();
				TryAttack();
			}
		}

		private void FixedUpdate() {
			if(playerData.IsAlive){
				Move();
			}
		}

		private void GetInput(){
			float xAxis = Input.GetAxisRaw("Horizontal");
			float yAxis = Input.GetAxisRaw("Vertical");
			input = new Vector2(xAxis, yAxis);
		}

		private void Move(){
			if(!playerData.IsAlive)
				return;
			rb.velocity = input.normalized * playerData.Speed;
			anim.SetIsMoving(input.magnitude != 0);
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
			if(Input.GetMouseButton(0) && canAttack){
				WeaponAttack.Attack(gameObject, playerData.CurrentWeapon);
				StartCoroutine(AttackCooldown(1/playerData.CurrentWeapon.AttackSpeed));
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