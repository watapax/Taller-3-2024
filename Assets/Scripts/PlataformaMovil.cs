using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
public class PlataformaMovil : MonoBehaviour
{
    public Transform splineAnimateObject;
    public Rigidbody2D rb2D;

    Vector3 velocidad;
    Vector3 prevPos;

    private void Awake()
    {

        rb2D = GetComponentInChildren<Rigidbody2D>();
        prevPos = splineAnimateObject.position;
    }
    
    private void FixedUpdate()
    {
        velocidad = (splineAnimateObject.position -prevPos) / Time.deltaTime;
        prevPos = splineAnimateObject.position;

        rb2D.velocity = velocidad;
        
    }
}

