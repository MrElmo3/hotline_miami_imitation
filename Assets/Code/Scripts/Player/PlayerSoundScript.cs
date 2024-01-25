// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerSoundScript : MonoBehaviour{

// 	private readonly float timeToDestroy = 0.5f;

// 	private void Start() {
// 		StartCoroutine("VanishSound");
// 	}

//     private void OnTriggerEnter2D(Collider2D other) {
// 		if(other.CompareTag("Enemy")){
// 			other.GetComponent<StateMachine>().playerSound = true;
// 		}
// 	}

// 	IEnumerator VanishSound(){
// 		yield return new WaitForSeconds(timeToDestroy);
// 		Destroy(this.gameObject);
// 	}
// }
