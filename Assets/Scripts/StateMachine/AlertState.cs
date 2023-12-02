using System.Collections.Generic;
using UnityEngine;

public class AlertState : MonoBehaviour{

	[SerializeField] private float speed;
	[SerializeField] private float _rotationSpeed;
	
	[SerializeField] private GraphScript graph;

	[SerializeField] private GameObject testTarget;

	private List<GameObject> path;
	private bool playerDetection;
	private int index;

	private void OnEnable() {
		graph = GameObject.FindWithTag("Graph").GetComponent<GraphScript>();
		Search(testTarget);
		index = 0;
	}

	private void Update() {
		if(!playerDetection){
			if(index < path.Count){
				Move(path[index].transform.position);
				RotateTowards(path[index].transform.position);
				
				if((transform.position - path[index].transform.position).magnitude < 0.05f)
					index++;
			}
			else{
				//return

			}
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

	private void Chase(){

	}

	private void Search(GameObject target){
		GameObject startNode = graph.GetNearNode(gameObject);
		GameObject endNode = graph.GetNearNode(target);

		path = graph.NearestPath(startNode, endNode);
	}
	
}
