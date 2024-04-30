using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MovilePlatformSpline : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Transform container;
    SplineAnimate splineAnimate;
    Vector2 velocidad;
    Vector2 posPrev;

    private void Start()
    {
        splineAnimate = container.GetComponent<SplineAnimate>();
        posPrev = container.position;
        rb2d.position = posPrev;
    }

    private void FixedUpdate()
    {
        velocidad = (posPrev - (Vector2)transform.position).normalized * splineAnimate.MaxSpeed;
        posPrev = container.position;
        
        rb2d.velocity = velocidad;
    }
}
