using UnityEngine;

namespace Enemy
{

public class AnimationController : MonoBehaviour {
	
	private bool isMoving = false;

	private Animator animator;

	void Start(){
		animator = GetComponent<Animator>();
	}

	void Update(){
		animator.SetBool("isMoving", isMoving);
	}

	public void SetIsMoving(bool isMoving){
		this.isMoving = isMoving;
	}
}

}