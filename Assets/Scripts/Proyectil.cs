using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public int damage;
    public GameObject impactPrefab;
    public float velocidadBala;
    RaycastHit2D hit;
    public Rigidbody2D rb2d;

    Vector3 posicionAnterior;
    SonidosGameObject sonidosGO;

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

        hit = Physics2D.Raycast(transform.position, direccion.normalized, direccion.magnitude);

        if(hit.collider != null)
        {
            if (hit.transform.GetComponent<Damagable>() != null)
                hit.transform.GetComponent<Damagable>().TakeDamage(damage);

            if (impactPrefab)
                Instantiate(impactPrefab, hit.point, Quaternion.identity);

            sonidosGO.ReproducirSonido();
            Destroy(gameObject);
        }

        

        //transform.Translate(transform.right * velocidadBala * Time.deltaTime, Space.World);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = transform.right * velocidadBala;
    }




}
