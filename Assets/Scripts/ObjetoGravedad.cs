using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoGravedad : MonoBehaviour
{
    float distanciaMinima = 6;
    bool aplicarFuerza;
    Rigidbody2D rb2D;
    bool agarrado;
    float startGravityScale;
    
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        startGravityScale = rb2D.gravityScale;
    }

    private void OnMouseDown()
    {
        //agarrar
        print("Agarro");
        // si lo toque revisar si estoy a distancia minima del player
        // sino chao pescao

        // si está a distancia el objeto aplica fuerza en direccion al player
        if(Vector3.Distance(transform.position , ArmaGravedad.posicionBrazo) < distanciaMinima)
        {
            aplicarFuerza = true;
            rb2D.gravityScale = 0;
            agarrado = true;
        }

    }

    private void OnMouseUp()
    {
        //Soltar
        print("Soltar");
        // si lo tengo agarrado lo lanza en dirección del pivote
        aplicarFuerza = false;
        rb2D.gravityScale = startGravityScale;
        
        if(agarrado)
        {
            rb2D.AddForce(Disparar.direccionRecoil.normalized * ArmaGravedad.fuerza, ForceMode2D.Impulse);
            agarrado = false;
        }
    }

    private void Update()
    {
        if(aplicarFuerza)
        {
            Vector2 direccion = (ArmaGravedad.posicionBrazo - transform.position).normalized;
            rb2D.AddForce(direccion);
        }
    }



}
