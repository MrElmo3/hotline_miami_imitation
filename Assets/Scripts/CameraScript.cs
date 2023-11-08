using Unity.Mathematics;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	[SerializeField] private float maxRotation = 4;

	private GameObject[] CameraPoints;
	private bool existLimits;

	private GameObject target;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		CameraPoints = GameObject.FindGameObjectsWithTag("CameraPoint");
		existLimits = CameraPoints.Length == 2 ? true : false;
	}

	// Update is called once per frame
	void Update()
	{
		CalcPosition();

		if(existLimits){
			CalcInclination();
		}
	}

	private void CalcInclination(){
		Vector2 diagonal = new Vector2(
			math.abs(CameraPoints[1].transform.position.x - CameraPoints[0].transform.position.x)/2,
			math.abs(CameraPoints[1].transform.position.y - CameraPoints[0].transform.position.y)/2);

		Vector2 centerMap = diagonal + new Vector2(CameraPoints[0].transform.position.x, CameraPoints[0].transform.position.y);
		Vector2 distanceCenter = new Vector2(transform.position.x,transform.position.y) - centerMap;

		float multiplicator = (distanceCenter.x / diagonal.x) * (distanceCenter.y / diagonal.y);
		
		transform.rotation = Quaternion.Euler(0, 0, maxRotation*multiplicator);
	}

	private void CalcPosition(){
		transform.position = new Vector3(
			target.transform.position.x,
			target.transform.position.y,
			-10);
	}
}
