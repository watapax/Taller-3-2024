using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaGravedad : MonoBehaviour
{
    public bool puedeUsarMecanica;
    public Transform pivoteBrazo;
    public float fuerzaLanzamiento;
    bool sostieneObjeto;
    public static Vector3 posicionBrazo;
    public static Vector3 direccionBrazo;
    public static float fuerza;

    private void Start()
    {
        fuerza = fuerzaLanzamiento;
    }
    private void Update()
    {
        if(!puedeUsarMecanica) return;
        posicionBrazo = pivoteBrazo.position;
        direccionBrazo = pivoteBrazo.right;

        
    }
}
