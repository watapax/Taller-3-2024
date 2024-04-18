using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class DisparoCargado : MonoBehaviour
{
    public bool disparoCargado;
    public float minTime,maxTime;
    
    float porcentaje;
    float lastTime;
    float t;
    
    bool cargando;
    bool puedeDisparar;

    public Disparar disparar;
    public LibreriaDeSonidos sonidosDisparos;
    public AudioClip sonidoDisparoFuerte;
    public AudioSource audioSource;
    public ParticleSystem ps;
    public float maxParticle;

    ParticleSystem.EmissionModule em;
    public Light2D luz;
    public float intensidadMinima, intensidadMaxima;
    public float maximaCadencia;

    private void Start()
    {
        em = ps.emission;
        luz.intensity = 0;
        em.rateOverTime = 0;
    }


    private void Update()
    {
        if (!disparoCargado) return;
        Cargar();
    }

    void Cargar()
    {
        if(Input.GetMouseButtonDown(0))
        {
            cargando = true;
            lastTime = Time.time;
        }
        if(Input.GetMouseButtonUp(0))
        {
            if (t > minTime)
            {
                if(puedeDisparar)
                    Disparar();
            }
            cargando = false;
            t = 0;
            luz.intensity = 0;
        }

        if(cargando)
        {
            if(Time.time > lastTime + minTime)
            {
                t += Time.deltaTime;
                t = Mathf.Clamp(t, 0, maxTime);
                porcentaje = t / maxTime; 
                luz.intensity = Mathf.Lerp(0, Random.Range(intensidadMinima * porcentaje, intensidadMaxima * porcentaje), porcentaje);
                puedeDisparar = true;
            }
        }

        em.rateOverTime = porcentaje * maxParticle;
        audioSource.volume = porcentaje;
        audioSource.pitch = porcentaje;
    }

    public void Disparar()
    {
        if(porcentaje < 0.5f)
        {
            SoundFXManager.instance.ReproducirSFX(sonidosDisparos);
        }
        else
        {
            SoundFXManager.instance.ReproducirSFX(sonidoDisparoFuerte);
        }

        disparar.Recoil(maximaCadencia * porcentaje);
        porcentaje = 0;
        puedeDisparar = false;
        
    }


}
