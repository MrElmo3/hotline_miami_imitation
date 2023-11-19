using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionCone : MonoBehaviour{
	[SerializeField] private Material VisionConeMaterial;

	[SerializeField] private float VisionRange;
	[SerializeField] private float VisionAngle;

	[SerializeField] private LayerMask VisionObstructingLayer;//layer with objects that obstruct the enemy view, like walls, for example

	[SerializeField] private int VisionConeResolution = 120;//the vision cone will be made up of triangles, the higher this value is the pretier the vision cone will be
	
	private Mesh VisionConeMesh;
	private MeshFilter MeshFilter_;

	private EdgeCollider2D VisionConeCollider;

	//Create all of these variables, most of them are self explanatory, but for the ones that aren't i've added a comment to clue you in on what they do
	//for the ones that you dont understand dont worry, just follow along

	void Start(){
		transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
		MeshFilter_ = transform.AddComponent<MeshFilter>();
		VisionConeCollider = transform.AddComponent<EdgeCollider2D>();
		VisionConeCollider.isTrigger = true;
		VisionConeMesh = new Mesh();
		VisionAngle *= Mathf.Deg2Rad;
	}

	void Update(){
		DrawVisionCone();//calling the vision cone function everyframe just so the cone is updated every frame
	}

	void DrawVisionCone(){//this method creates the vision cone mesh

		int[] triangles = new int[(VisionConeResolution - 1) * 3];
		Vector3[] meshVertices = new Vector3[VisionConeResolution + 1];
		Vector2[] colliderPoints = new Vector2[VisionConeResolution + 1];

		meshVertices[0] = Vector3.zero;
		colliderPoints[0] = Vector2.zero;

		float Currentangle = -VisionAngle / 2;
		float angleIcrement = VisionAngle / (VisionConeResolution - 1);
		float Sine;
		float Cosine;

		for (int i = 0; i < VisionConeResolution; i++){

			Sine = Mathf.Sin(Currentangle);
			Cosine = Mathf.Cos(Currentangle);

			Vector2 RaycastDirection = (transform.up * Cosine) + (transform.right * Sine);
			Vector2 VertForward = (Vector2.up * Cosine) + (Vector2.right * Sine);

			RaycastHit2D hit = Physics2D.Raycast(transform.position, RaycastDirection, VisionRange, VisionObstructingLayer );

			if(hit.collider != null){
				meshVertices[i + 1] = VertForward * hit.distance;
				colliderPoints[i + 1] = VertForward * hit.distance;
			}
			else{
				meshVertices[i + 1] = VertForward * VisionRange;
				colliderPoints[i + 1] = VertForward * VisionRange;
			}

			Currentangle += angleIcrement;
		}

		for (int i = 0, j = 0; i < triangles.Length; i += 3, j++){
			triangles[i] = 0;
			triangles[i + 1] = j + 1;
			triangles[i + 2] = j + 2;
		}

		VisionConeMesh.Clear();
		VisionConeMesh.vertices = meshVertices;
		VisionConeCollider.points = colliderPoints;
		VisionConeMesh.triangles = triangles;
		MeshFilter_.mesh = VisionConeMesh;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player"){
			Debug.Log(true);
		}
	}

}