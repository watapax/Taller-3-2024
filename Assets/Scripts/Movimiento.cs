using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    Rigidbody2D rb;
    float moveInput;
    public float moveSpeed;
    private float acceleration;
    private float decceleration;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void GetInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    private void Update()
    {
        GetInput();
    }

    void Moverse()
    {

    }


    private void FixedUpdate()
    {
        Moverse();
    }
}
