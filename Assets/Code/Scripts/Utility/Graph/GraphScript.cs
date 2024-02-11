using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class of the graph, store the nodes and some methods to work with them
/// </summary>
public class GraphScript : MonoBehaviour{

	/// <summary>
	/// Colision layer for the creation of the conections between the nodes
	/// </summary>
	[SerializeField]private LayerMask actionLayer;

	private List<GameObject> nodes;

	private void Awake() {
		nodes = new List<GameObject>(GameObject.FindGameObjectsWithTag("GraphNode"));
		CreateConections();
	}

	// private void Update() {
	// 	CreateConections();
	// }

	/// <summary>
	/// Get the nearest node to the entity
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public GameObject GetNearNode(GameObject entity){
		float minDistance = int.MaxValue;
		GameObject target = null;

		foreach (GameObject node in nodes){
			float auxDistance = Vector2.Distance(node.transform.position, entity.transform.position);

			if(minDistance > auxDistance){
				target = node;
				minDistance = auxDistance;
			}
		}
		return target;
	}

	// public GameObject GetNearNode(Vector2 position){
	// 	float minDistance = int.MaxValue;
	// 	GameObject target = null;

	// 	foreach (GameObject node in nodes){
	// 		float auxDistance = Vector2.Distance(node.transform.position, position);

	// 		if(minDistance > auxDistance){
	// 			target = node;
	// 			minDistance = auxDistance;
	// 		}
	// 	}
	// 	return target;
	// }

	/// <summary>
	/// Create the conections between the nodes using the layer mask
	/// </summary>
	private void CreateConections(){
		for (int i = 0; i < nodes.Count; i++){
			for (int j = i+1; j < nodes.Count; j++){

				Vector2 RaycastDirection = nodes[j].transform.position - nodes[i].transform.position;
				RaycastHit2D hit = Physics2D.Raycast(nodes[i].transform.position, RaycastDirection.normalized, 200, actionLayer);

				if(hit.transform != nodes[j].transform )
					continue;
				
				Debug.DrawRay(nodes[i].transform.position, RaycastDirection, color:Color.red);

				nodes[i].GetComponent<NodeScript>().CreateConection(nodes[j]);
				nodes[j].GetComponent<NodeScript>().CreateConection(nodes[i]);

			}
		}
	}

	private float Cost(GameObject node1, GameObject node2){
		return Vector2.Distance(node1.transform.position, node2.transform.position);
	}

	private float Heuristic(GameObject node1, GameObject node2){
		return Math.Abs(node1.transform.position.x - node2.transform.position.x) 
			+ 
			Math.Abs(node1.transform.position.y - node2.transform.position.y);
	}

	/// <summary>
	/// Uses the A* algorithm to find the nearest path between two nodes
	/// </summary>
	/// <param name="start"></param>
	/// <param name="end"></param>
	/// <returns>A list of nodes with the path</returns>
	public List<GameObject> NearestPath(GameObject start, GameObject end){
		PriorityQueue<GameObject> frontier;
		Dictionary<GameObject, GameObject> cameFrom;
		Dictionary<GameObject, float> costSoFar;
		
		frontier = new PriorityQueue<GameObject>();
		cameFrom = new Dictionary<GameObject, GameObject>();
		costSoFar= new Dictionary<GameObject, float>();
		//esto esta puesto para fozar la compilacion
		frontier.Enqueue(start, 0);
		cameFrom.Add(start, start);
		costSoFar.Add(start, 0);

		while(frontier.Count != 0){
			GameObject current = frontier.Dequeue();

			if(current == end)
				break;

			foreach (GameObject next in current.GetComponent<NodeScript>().GetConections()){
				float newCost = costSoFar[current] + Cost(current, next);

				if(!costSoFar.ContainsKey(next) || newCost < costSoFar[next]){
					costSoFar[next] = newCost;
					float priority = newCost + Heuristic(end, next);
					frontier.Enqueue(next, priority);
					cameFrom[next] = current;
				}
			}
		}

		List<GameObject> path = new List<GameObject>();
		GameObject aux = end;
		while( aux !=  start){
			path.Insert(0,aux);
			aux = cameFrom[aux];
			
		}
		path.Insert(0, start);

		return path;
	}

}
