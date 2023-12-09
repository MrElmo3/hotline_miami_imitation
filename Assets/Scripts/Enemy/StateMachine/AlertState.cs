using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class AlertState : MonoBehaviour{

	[SerializeField] private float speed = 12f;
	[SerializeField] private float _rotationSpeed = 270f;

	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float _reactionTime = 0.25f;
	[SerializeField] private float timeBetweenShots = 0.5f;
	private Transform firePivot;
	private float lastTimeShot = 0;
	private AudioSource pistolShot;

	private GraphScript graph;
	private StateMachine stateMachine;
	private GameObject player;

	[SerializeField] private List<GameObject> path;
	private int index;
	private bool isSearching;

	private void OnEnable() {
		if(player == null){
			stateMachine = GetComponent<StateMachine>();
			graph = GameObject.FindWithTag("Graph").GetComponent<GraphScript>();
			player = GameObject.FindWithTag("Player");
		}
		isSearching = true;
		index = 0;
		Search();
	}

	private void Start() {
		firePivot = transform.GetChild(0);
		pistolShot = GetComponent<AudioSource>();
	}

	private void Update() {
		//Busqueda
		if(!stateMachine.playerView && player.GetComponent<PlayerScript>().IsAlive()){
			if(index < path.Count){
				Move(path[index].transform.position);
				RotateTowards(path[index].transform.position);
				
				if((transform.position - path[index].transform.position).magnitude < 0.05f)
					index++;
			}
			else
				isSearching = false;
		}

		//Disparo
		else if(stateMachine.playerView && player.GetComponent<PlayerScript>().IsAlive()){
			Shoot();
		}
		
		else if(!isSearching || !player.GetComponent<PlayerScript>().IsAlive()){
			//hace el recorrido inverso al path y retorna al estado anterior
			index = index >= path.Count ? path.Count - 1 : index;
			Move(path[index].transform.position);
			RotateTowards(path[index].transform.position);
			if((transform.position - path[0].transform.position).magnitude < 0.05f){
				stateMachine.playerSound = false;
				stateMachine.EnableState(stateMachine.GetPreviousState());
			}
			
			if((transform.position - path[index].transform.position).magnitude < 0.05f)
				index--;
		}
	}
	
	private void Move( Vector3 target){
		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}

	private void RotateTowards(Vector3 target){
		Vector2 distance = target - transform.position;
		Quaternion targetRotation = Quaternion.LookRotation(transform.forward, distance.normalized);
		Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
		transform.rotation = rotation;
	}

	private void Shoot(){
		RotateTowards(player.transform.position);
		firePivot.transform.GetPositionAndRotation(out Vector3 position, out Quaternion rotation);
		rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 90);
		bool canShoot = Time.time >= lastTimeShot + timeBetweenShots + _reactionTime;

		if (canShoot){
			lastTimeShot = Time.time;
			GameObject bullet = Instantiate(bulletPrefab, position, rotation);
			bullet.GetComponent<EnemyBullet>().enabled = true;
			pistolShot.Play();
		}
	}

	private void Search(){
		GameObject startNode = graph.GetNearNode(gameObject);
		GameObject endNode = graph.GetNearNode(player);

		path = graph.NearestPath(startNode, endNode);
	}
	
}
