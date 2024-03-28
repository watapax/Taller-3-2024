using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class IntensidadLuzRandom : MonoBehaviour
{

    Light2D luz;
    

    public float frecuencia, min, max;

    float t;
    float valor;
    float startFreq;

    private void Awake()
    {
        luz = GetComponent<Light2D>();
        startFreq = frecuencia;
        valor = Random.Range(min, max);
    }

    private void Update()
    {
        t += Time.deltaTime;
        luz.intensity = Mathf.Lerp(luz.intensity, valor, Time.deltaTime * 10);
        if(t >frecuencia)
        {
            valor = Random.Range(min, max);
            t = 0;
            frecuencia = Random.Range(startFreq - 0.1f, startFreq + 0.1f);
        }

    }

}
