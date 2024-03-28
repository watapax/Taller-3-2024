using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public LibreriaDeSonidos sonidosDisparo;
    public GameObject balaPrefab;
    public GameObject muzzleFlash;
    public Transform spawnPoint;
    public Transform pivoteArma;
    public float tiempoCadencia;
    public float fuerzaCadencia;
    public bool seguirMouse;

    public float t;
    public Rigidbody2D rb2d;
    ControlesPlayer cp;
    bool facingRight = true;
    private void Awake()
    {
        cp = GetComponent<ControlesPlayer>();
    }



    public void Shoot()
    {
        
        GameObject bala = Instantiate(balaPrefab, spawnPoint.position, spawnPoint.rotation);
        if(!facingRight) bala.transform.right = -bala.transform.right;
        Instantiate(muzzleFlash, spawnPoint.position, spawnPoint.rotation);
        SoundFXManager.instance.ReproducirSFX(sonidosDisparo);
        t = 0;  
        //Recoil();
    }

    private void Recoil()
    {
        rb2d.AddForce(-transform.right * fuerzaCadencia, ForceMode2D.Impulse);
    }

    private void Update()
    {
        t += Time.deltaTime;
        SeguirMouse();
    }

    void SeguirMouse()
    {
        if (!seguirMouse) return;
        
        Vector3 mp = Input.mousePosition;
        mp.z = Math.Abs(Camera.main.transform.position.z - pivoteArma.position.z);

        Vector3 wmp = Camera.main.ScreenToWorldPoint(mp);
        Vector2 direccion = wmp - pivoteArma.position;
        pivoteArma.right = facingRight? direccion : -direccion;

        //hacer el flip aca
        if ((wmp.x > transform.position.x && !facingRight) || (wmp.x < transform.position.x && facingRight))
            Flip();

    }

    public void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        facingRight = !facingRight;
        transform.localScale = localScale;    
    }

}
