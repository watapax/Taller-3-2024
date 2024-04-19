using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public bool puedeRebotar;
    public int rebotesMaximos;
    public int damage;
    public GameObject impactPrefab;
    public float velocidadBala;
    RaycastHit2D hit;
    public Rigidbody2D rb2d;
    public LayerMask layerMask;

    Vector3 posicionAnterior;
    SonidosGameObject sonidosGO;
    int rebotes;
    private void Awake()
    {
        sonidosGO = GetComponent<SonidosGameObject>();
    }

    IEnumerator Start()
    {
        posicionAnterior = transform.position;
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }

    private void Update()
    {
        Vector2 direccion = (transform.position - posicionAnterior);
        posicionAnterior = transform.position;

        hit = Physics2D.Raycast(transform.position, direccion.normalized, direccion.magnitude, layerMask);

        if(hit.collider != null)
        {
            if (hit.transform.GetComponent<Damagable>() != null)
                hit.transform.GetComponent<Damagable>().TakeDamage(damage);

            if (impactPrefab)
                Instantiate(impactPrefab, hit.point, Quaternion.identity);

            if(puedeRebotar)
            {
                transform.right = Vector3.Reflect(transform.right, hit.normal);
                if (rebotes > rebotesMaximos)
                    Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            

            sonidosGO.ReproducirSonido();
            rebotes++;

        }

        

        //transform.Translate(transform.right * velocidadBala * Time.deltaTime, Space.World);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = transform.right * velocidadBala;
    }




}
