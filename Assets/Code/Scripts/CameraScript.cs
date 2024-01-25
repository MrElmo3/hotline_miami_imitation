using Unity.Mathematics;
using UnityEngine;

public class CameraScript : MonoBehaviour{

	[SerializeField] private float maxRotation = 4;

	private GameObject CameraPointU;
	private GameObject CameraPointL;
	private bool existPoints;
	private GameObject target;

	void Start(){
		target = GameObject.FindGameObjectWithTag("Player");
		CameraPointL = GameObject.Find("CameraPointL");
		CameraPointU = GameObject.Find("CameraPointU");
		existPoints = CameraPointL && CameraPointU;
	}

	void Update(){
		CalcPosition();

		if(existPoints)
			CalcInclination();
	}

	private void CalcInclination(){
		Vector3 actualPosition = transform.position;
		Vector3 positionL = CameraPointL.transform.position;
		Vector3 positionU = CameraPointU.transform.position;

		Vector2 diagonal = new Vector2(
			math.abs(positionU.x - positionL.x)/2,
			math.abs(positionU.y - positionL.y)/2);

		Vector2 centerMap = diagonal + 
			new Vector2(
				positionL.x, 
				positionL.y
				);

		Vector2 distanceCenter = new Vector2(actualPosition.x,actualPosition.y) - centerMap;
		float multiplicator = distanceCenter.x / diagonal.x * (distanceCenter.y / diagonal.y);
		
		transform.rotation = Quaternion.Euler(0, 0, maxRotation*multiplicator);
	}

	private void CalcPosition(){
		// if(!target.GetComponent<PlayerScript>().IsAlive())
		// 	return;
		
		transform.position = new Vector3(
			target.transform.position.x,
			target.transform.position.y,
			-10);
	}
}
