using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour{

    private Animator animator;

    [SerializeField] private WeaponsEnum weapon = WeaponsEnum.UNARMED;
    [SerializeField] private bool isMoving = false;

    void Start(){
        animator = GetComponent<Animator>();
    }


    void Update(){
        animator.SetFloat("WeaponType", (float)weapon/2);
        animator.SetBool("isMoving", isMoving);
    }
}
