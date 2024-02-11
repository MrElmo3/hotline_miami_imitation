// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyBullet : MonoBehaviour{

// 	[SerializeField] float bulletSpeed;
// 	[SerializeField] private float timeToDestroy;
// 	[SerializeField] GameObject hitSound;
// 	private Rigidbody2D rb;

// 	void Start(){
// 		rb = GetComponent<Rigidbody2D>();
// 		StartCoroutine("DestroyBullet");
// 	}

// 	void FixedUpdate(){
// 		rb.velocity = this.transform.right * bulletSpeed;
// 	}

// 	private void OnTriggerEnter2D(Collider2D collision){
// 		if (collision.transform.CompareTag("Player")){
// 			GameObject hit = Instantiate(hitSound, transform.position, Quaternion.identity);
// 			Destroy(hit, 2f);
// 			collision.gameObject.GetComponent<PlayerScript>().Die();
// 			Destroy(this.gameObject);
// 			StopCoroutine("DestroyBullet");
// 		}
// 		else if (collision.transform.CompareTag("Enviroment"))
// 		{
// 			Destroy(this.gameObject);
// 			StopCoroutine("DestroyBullet");
// 		}
// 	}

// 	IEnumerator DestroyBullet(){
// 		yield return new WaitForSeconds(timeToDestroy);
// 		Destroy(this.gameObject);
// 	}
// }
