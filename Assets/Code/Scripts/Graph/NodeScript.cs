using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
	[SerializeField] private List<GameObject> conections;

	public bool IsConnected(GameObject node){
		return node.GetComponent<NodeScript>().conections.Contains(node);
	}

	public void CreateConection(GameObject target){
		if(target == this || conections.Contains(target))
			return;
		conections.Add(target);
	}

	public List<GameObject> GetConections(){
		return conections;
	}
}
