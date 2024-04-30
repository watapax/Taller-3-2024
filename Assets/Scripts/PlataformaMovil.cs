using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
public class PlataformaMovil : MonoBehaviour
{
    VelocidadObjeto velocidadObjeto;
    Rigidbody2D rb2D;
    SplineAnimate splnAnim;
    public Vector2 velocidadPlataforma;
    private void Awake()
    {
        velocidadObjeto = GetComponentInChildren<VelocidadObjeto>();
        rb2D = GetComponentInChildren<Rigidbody2D>();
        splnAnim = GetComponentInChildren<SplineAnimate>();
    }
    
    private void FixedUpdate()
    {
        rb2D.velocity = velocidadObjeto.Velocidad;
        velocidadPlataforma = rb2D.velocity;
    }
}

