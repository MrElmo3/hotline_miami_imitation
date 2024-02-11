using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class of the node, store the information of the conections
/// </summary>
public class NodeScript : MonoBehaviour{

	[SerializeField] private List<GameObject> conections;

	/// <param name="node"></param>
	/// <returns>
	/// True if the node is conected to this,
	/// false if not
	/// </returns>
	public bool IsConnected(GameObject node){
		return node.GetComponent<NodeScript>().conections.Contains(node);
	}

	/// <summary>
	/// Creates a conection between this and the target node
	/// </summary>
	/// <param name="target"></param>
	public void CreateConection(GameObject target){
		if(target == this || conections.Contains(target))
			return;
		conections.Add(target);
	}

	/// <summary>
	/// List of all the conections of this node
	/// </summary>
	/// <returns></returns>
	public List<GameObject> GetConections(){
		return conections;
	}
}
