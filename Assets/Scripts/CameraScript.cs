using Unity.Mathematics;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	[SerializeField] float maxRotation = 10;
	private GameObject CameraPoint1;
	private GameObject CameraPoint2;


	private GameObject target;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		CameraPoint1 = GameObject.FindGameObjectWithTag("CameraPoint1");
		CameraPoint2 = GameObject.FindGameObjectWithTag("CameraPoint2");
	}

	// Update is called once per frame
	void Update()
	{
		CalcPosition();
		CalcInclination();
	}

	private void CalcInclination(){
		Vector2 diagonal = new Vector2(
			math.abs(CameraPoint1.transform.position.x - CameraPoint2.transform.position.x)/2,
			math.abs(CameraPoint1.transform.position.y - CameraPoint2.transform.position.y)/2);
		Vector2 centerMap = diagonal + new Vector2(CameraPoint2.transform.position.x, CameraPoint2.transform.position.y);
		Vector2 distanceCenter = new Vector2(target.transform.position.x,target.transform.position.y) - centerMap;

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
