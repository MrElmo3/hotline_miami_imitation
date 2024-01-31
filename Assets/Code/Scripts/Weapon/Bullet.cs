using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

	[SerializeField] float bulletSpeed;
	[SerializeField] private float timeToDisable;
	private Rigidbody2D rb;

	private void OnEnable() {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * bulletSpeed;
		StartCoroutine("DisableBullet");
	}
	
	void Update(){
		// CheckColission();
    }
	// private void CheckColission(){
	// 	RaycastHit2D hitf = Physics2D.Raycast(transform.position, transform.right*-1, 1.5f, LayerMask.GetMask("Walls"));

	// 	if (hitf.collider != null)
	// 	{
	// 		StopCoroutine("DestroyBullet");
	// 		Destroy(this.gameObject);
	// 	}
	// }
	// private void OnTriggerEnter2D(Collider2D collision){
	// 	if(collision.CompareTag("Enemy")){
	// 		StopCoroutine("DestroyBullet");
	// 		Destroy(collision.gameObject);
	// 		Destroy(this.gameObject);
	// 	}
	// 	else if(collision.transform.CompareTag("Enviroment"))
	// 	{
	// 		StopCoroutine("DestroyBullet");
	// 		Destroy(this.gameObject);
	// 	}
	// }

	IEnumerator DisableBullet(){
		yield return new WaitForSeconds(timeToDisable);
		BulletSpawner.Instance.AddBullet(this.gameObject);
	}

}
