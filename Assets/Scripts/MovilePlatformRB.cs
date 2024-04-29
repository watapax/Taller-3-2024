using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovilePlatformRB : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public Transform waypointParent;
    Transform[] waypoints;
    Vector3 direccion;
    public float velocidad;

    int c;
    int index;
    float minDistance = 0.1f;
    Vector3 target;
    private void Awake()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        rb2d.position = waypoints[0].position;
        GetNewDirection();
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(rb2d.position, target) < minDistance) 
        {
            GetNewDirection();
        }
        rb2d.velocity = direccion * velocidad;
    }

    public void GetNewDirection()
    {
        
        target = waypoints[index].position;
        direccion = ((Vector2)target - rb2d.position).normalized;
        c++;
        index = c% waypoints.Length;

    }


}
