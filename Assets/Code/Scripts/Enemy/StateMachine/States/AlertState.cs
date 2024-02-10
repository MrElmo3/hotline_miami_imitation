using UnityEngine;

namespace Enemy
{

public class AlertState : EnemyBaseState{

    public AlertState(){
        name = EnemyState.Alert;
    }
	
    public override void EnterState(EnemyStateMannager enemy){
        Debug.Log("AlertState");
    }

    public override void UpdateState(EnemyStateMannager enemy){
    }
}

}

// public class AlertState : MonoBehaviour{

// 	[SerializeField] private float speed = 12f;
// 	[SerializeField] private float _rotationSpeed = 270f;

// 	[SerializeField] private GameObject bulletPrefab;
// 	[SerializeField] private float _reactionTime = 0.65f;
// 	[SerializeField] private float timeBetweenShots = 0.5f;
// 	private Transform firePivot;
// 	private float lastTimeShot = 0;
// 	private float lastTimeSee;
// 	private AudioSource pistolShot;

// 	private GraphScript graph;
// 	private StateMachine stateMachine;

// 	[SerializeField] private List<GameObject> path;
// 	private int index;
// 	private bool isSearching;

// 	private void OnEnable() {
// 		if(stateMachine == null){
// 			stateMachine = GetComponent<StateMachine>();
// 			graph = GameObject.FindWithTag("Graph").GetComponent<GraphScript>();
// 		}
// 		isSearching = true;
// 		index = 0;
// 		Search();
// 	}

// 	private void Start() {
// 		firePivot = transform.GetChild(0);
// 		pistolShot = GetComponent<AudioSource>();
// 	}

// 	private void Update() {
// 		//Busqueda
// 		if(!stateMachine.playerView && stateMachine.GetPlayer().IsAlive() && isSearching){
// 			Debug.Log("Buscando");
// 			if(index < path.Count){
// 				Move(path[index].transform.position);
// 				RotateTowards(path[index].transform.position);
				
// 				if((transform.position - path[index].transform.position).magnitude < 0.05f)
// 					index++;
// 			}
// 			else
// 				isSearching = false;
// 		}

// 		//Disparo
// 		else if(stateMachine.playerView && stateMachine.GetPlayer().IsAlive()){
// 			stateMachine.getAnimator().SetBool("isWalking", false);
// 			Debug.Log("Disparando");
// 			if(lastTimeSee == 0) lastTimeSee = Time.time;
// 			Shoot();
// 		}
		
// 		//Regreso al estado anterior
// 		else if(!isSearching || !stateMachine.GetPlayer().IsAlive()){
// 			//hace el recorrido inverso al path y retorna al estado anterior
// 			Debug.Log("Regresando");
// 			index = index >= path.Count ? path.Count - 1 : index;
// 			Move(path[index].transform.position);
// 			RotateTowards(path[index].transform.position);
// 			if((transform.position - path[0].transform.position).magnitude < 0.05f){
// 				stateMachine.playerSound = false;
// 				stateMachine.EnableState(stateMachine.GetPreviousState());
// 			}
			
// 			if((transform.position - path[index].transform.position).magnitude < 0.05f)
// 				index--;
// 		}

// 		if(!stateMachine.playerView) lastTimeSee = 0;
// 	}
	
// 	private void Move( Vector3 target){
// 		stateMachine.getAnimator().SetBool("isWalking", true);
// 		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
// 	}

// 	private void RotateTowards(Vector3 target){
// 		Vector2 distance = target - transform.position;
// 		Quaternion targetRotation = Quaternion.LookRotation(transform.forward, distance.normalized);
// 		Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
// 		transform.rotation = rotation;
// 	}

// 	private void Shoot(){
// 		RotateTowards(stateMachine.GetPlayer().transform.position);
// 		firePivot.transform.GetPositionAndRotation(out Vector3 position, out Quaternion rotation);
// 		rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 90);
// 		bool canShoot = 
// 			Time.time >= lastTimeShot + timeBetweenShots && 
// 			Time.time >= lastTimeSee + _reactionTime;

// 		if (canShoot){
// 			Debug.Log("Set");
// 			stateMachine.getAnimator().SetTrigger("shoot");
// 			lastTimeShot = Time.time;
// 			Instantiate(bulletPrefab, position, rotation);
// 			pistolShot.Play();
// 		}
// 	}

// 	private void Search(){
// 		GameObject startNode = graph.GetNearNode(gameObject);
// 		GameObject endNode = graph.GetNearNode(stateMachine.GetPlayer().gameObject);

// 		Debug.Log(startNode.name);
// 		Debug.Log(endNode.name);

// 		path = graph.NearestPath(startNode, endNode);
// 	}
	
// }
