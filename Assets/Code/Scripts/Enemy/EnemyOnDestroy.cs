// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyOnDestroy : MonoBehaviour
// {
//     [SerializeField] private GameObject hitSound;
//     [SerializeField] private GameObject handgun;
//     private PlayerScript player;

//     private bool isAppQuitting = false;
//     private void Start()
//     {
//         player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
//     }
//     private void OnApplicationQuit()
//     {
//         isAppQuitting = true;
//         Debug.Log(isAppQuitting);
//     }
//     void OnDestroy()
//     {
//         if (!this.gameObject.scene.isLoaded) return;
//         try{
// 			GameManager.instance.EnemiesInGame--;
// 		}catch{
// 			Debug.Log("No se ha encontrado el GameManager");
// 		}
// 		GameObject hit = Instantiate(hitSound, player.transform.position, Quaternion.identity);
//         Instantiate(handgun, this.transform.position, Quaternion.identity);
// 		Destroy(hit, 2f);
//     }
// }
