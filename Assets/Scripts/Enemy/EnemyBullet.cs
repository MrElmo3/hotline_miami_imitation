using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour{

	[SerializeField] float bulletSpeed;
	[SerializeField] private float timeToDestroy;
	private Rigidbody2D rb;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine("DestroyBullet");
	}

	void FixedUpdate(){
		rb.velocity = this.transform.right * bulletSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.transform.CompareTag("Player")){
			collision.gameObject.GetComponent<PlayerScript>().Die();
			Destroy(this.gameObject);
			StopCoroutine("DestroyBullet");
		}
		else if (collision.transform.CompareTag("Enviroment"))
		{
			Destroy(this.gameObject);
			StopCoroutine("DestroyBullet");
		}
	}

	IEnumerator DestroyBullet(){
		yield return new WaitForSeconds(timeToDestroy);
		Destroy(this.gameObject);
	}
}
