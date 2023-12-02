using System.Collections.Generic;
using UnityEngine;

public class GraphScript : MonoBehaviour{

	[SerializeField]private List<GameObject> nodes;
	[SerializeField]private LayerMask actionLayer;
	
	void Start(){
		nodes = new List<GameObject>(GameObject.FindGameObjectsWithTag("GraphNode"));
		CreateConections();
	}

	public GameObject GetNearNode(GameObject entity){
		float minDistance = int.MaxValue;
		GameObject target = null;

		foreach (GameObject node in nodes){
			float auxDistance = Vector2.Distance(node.transform.position, entity.transform.position);

			if(minDistance < auxDistance){
				target = node;
				minDistance = auxDistance;
			}
		}
		return target;
	}

	public void CreateConections(){
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

	public List<GameObject> NearestPath(GameObject start, GameObject end){
		
		return null;
	}

}
