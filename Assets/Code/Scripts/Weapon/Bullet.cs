using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{

	[SerializeField] float bulletSpeed;
	[SerializeField] private float timeToDestroy;
	private Rigidbody2D rb;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine("DestroyBullet");
		rb.velocity = transform.right * bulletSpeed;
	}
	
	void Update(){
		// CheckColission();
    }

	public void SetDirection(Vector2 direction){
		transform.right = direction;
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

	IEnumerator DestroyBullet(){
		yield return new WaitForSeconds(timeToDestroy);
		Destroy(this.gameObject);
	}
}
