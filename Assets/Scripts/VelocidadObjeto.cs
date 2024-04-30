using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocidadObjeto : MonoBehaviour
{

    Vector3 velocidad;
    Vector3 prevPos;

    void Start() => prevPos = transform.position;

    void FixedUpdate()
    {
        velocidad = (transform.position - prevPos) / Time.deltaTime;
        prevPos = transform.position;
    }


    public Vector3 Velocidad
    {
        get { return velocidad; }
    }

}
