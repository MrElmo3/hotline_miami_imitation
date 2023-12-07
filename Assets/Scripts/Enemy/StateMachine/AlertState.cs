using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlertState : MonoBehaviour{

	[SerializeField] private float speed = 12;
	[SerializeField] private float _rotationSpeed = 270;
	
	private GraphScript graph;
	private StateMachine stateMachine;
	private GameObject player;

	private List<GameObject> path;
	private int index;
	private bool isSearching;

	private void Start() {
		stateMachine = GetComponent<StateMachine>();
		graph = GameObject.FindWithTag("Graph").GetComponent<GraphScript>();
		player = GameObject.FindWithTag("Player");
	}
	

	private void OnEnable() {
		isSearching = true;
		index = 0;
		try{
			Search();
		}catch{
			Debug.Log("No se ha encontrado un camino o se a detectado al jugador");
		}
	}

	private void Update() {
		if(!stateMachine.playerView){
			if(index < path.Count && isSearching){
				Move(path[index].transform.position);
				RotateTowards(path[index].transform.position);
				
				if((transform.position - path[index].transform.position).magnitude < 0.05f)
					index++;
			}
			else{
				//hace el recorrido inverso al path y retorna al estado anterior
				isSearching = false;
				index = index >= path.Count ? path.Count - 1 : index;
				Move(path[index].transform.position);
				RotateTowards(path[index].transform.position);

				if((transform.position - path[0].transform.position).magnitude < 0.05f)
					stateMachine.EnableState(stateMachine.patrolState);
				
				if((transform.position - path[index].transform.position).magnitude < 0.05f)
					index--;
			}
		}
		else{
			Shoot();
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
	}

	private void Search(){
		GameObject startNode = graph.GetNearNode(gameObject);
		GameObject endNode = graph.GetNearNode(player);

		path = graph.NearestPath(startNode, endNode);
	}
	
}
