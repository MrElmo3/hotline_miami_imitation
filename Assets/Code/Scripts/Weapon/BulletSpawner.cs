using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {

	private Queue<GameObject> bullets;
	[SerializeField] private GameObject bulletPrefab;

	public static BulletSpawner Instance;

	private void Start() {
		bullets = new Queue<GameObject>();
		Instance = this;
	}

	public void SpawnBullet(Vector3 position, Quaternion rotation){
		if(bullets.Count > 0){
			GameObject bullet = bullets.Dequeue();
			bullet.transform.position = position;
			bullet.transform.rotation = rotation;
			bullet.SetActive(true);
			return;
		}
		GameObject newBullet = Instantiate(bulletPrefab, position, rotation);
	}

	public void AddBullet(GameObject bullet){
		bullet.SetActive(false);
		bullets.Enqueue(bullet);
	}

}