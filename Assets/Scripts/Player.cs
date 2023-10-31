using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float timeToStop;
    [SerializeField]
    private float speed;
    private float xAxis;
    private float yAxis;
    private Vector2 inputVector;
    private Vector2 movementVector;
    private Vector2 currentVelocity;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        Aim();
    }
    private void FixedUpdate()
    {
        movementVector = Vector2.SmoothDamp(movementVector, inputVector, ref currentVelocity, timeToStop);
        Move();
    }

    private void Move()
    {
        rb.velocity = movementVector * speed;
    }

    private void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        inputVector = new Vector2(xAxis, yAxis).normalized;
    }

    private void Aim()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 distance = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle+90);
    }
    
}
