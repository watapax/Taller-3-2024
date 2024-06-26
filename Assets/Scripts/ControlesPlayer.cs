using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesPlayer : MonoBehaviour
{
    //Public
    public bool puedeDisparar, puedeMoverse, puedeSaltar;
    public KeyCode botonDisparo, botonSalto;
    public LayerMask layerPiso;

    public DatosSalto datosSalto;
    public Animator anim;

    public float velocidadMovimiento;
    public AudioClip sonidoSalto, sonidoAterrizaje;
    public LibreriaDeSonidos sonidosPasos;
    //Private

    Rigidbody2D rb2d;
    Disparar disparar;
    float horizontal;
    float gravedad;
    public float tiempoEntrePasos;
    float tiempoUltimoPaso;

    public bool grounded;


    Collider2D col2D;
    PrevenirDispararPiso prevenirDispararPiso;

    bool checkCayendo;
    bool saltando;

    // esto es para chequear el da�o de caida
    Vector3 posicionAnterior;
    Vector3 direccion;
    int sueloCount;
    Collider2D col;
    Vector2[] groundCheckPos;
    bool prevGround;
    public float velocidadDa�oCaida;

    // esto es para aplicar una fuerza externa (recoil)
    Vector2 fuerzaExterna;
    public float disipadorFuerzaExterna;
    public static Vector3 posPlayer;
    public Rigidbody2D movilePlatformRb;

    bool disableVelocityInput;


    private void Awake()
    {
        groundCheckPos = new Vector2[3];
        col = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        disparar = GetComponent<Disparar>();
        gravedad = Physics2D.gravity.y;
        prevenirDispararPiso = GetComponentInChildren<PrevenirDispararPiso>();
        CheckPointSystem.instance.ActualizarUltimaPos(transform.position);

        posicionAnterior = transform.position;

        

    }

    private void Update()
    {
        posPlayer = transform.position;
        CheckSuelo();
        CheckDa�oCaida();
        Saltar();
        Moverse();
        DisiparFuerzaExterna();
        Disparar();
        DatosAnimator();

        //Debug
        TestearFuerza();
    }

    void DatosAnimator()
    {
        anim.SetBool("ground", grounded);
        anim.SetFloat("velocidadX", (rb2d.velocity.x != 0)&&(horizontal !=0) ? 1 : 0);
    }

    void Saltar()
    {
        if (!puedeSaltar) return;


        if(grounded && Input.GetKeyDown(botonSalto))
        {
            float impulsoPlataforma = 0;
            if(movilePlatformRb != null)
            {
                impulsoPlataforma = movilePlatformRb.velocity.y >0? movilePlatformRb.velocity.y:0 ;
                movilePlatformRb = null;
            }
            rb2d.velocity = new Vector2(rb2d.velocity.x,  datosSalto.velocidadSalto + impulsoPlataforma);
            SoundFXManager.instance.ReproducirSFX(sonidoSalto);
            StartCoroutine(CheckAterrizaje());
            saltando = true;
        }


        if (rb2d.velocity.y < 0)
            rb2d.velocity += new Vector2(0,  gravedad * (datosSalto.multiplicadorCaida - 1) * Time.deltaTime);
        else if (rb2d.velocity.y > 0 && !Input.GetKey(botonSalto))
            rb2d.velocity += new Vector2(0, gravedad * (datosSalto.multiplicadorSaltoBajo - 1) * Time.deltaTime);



    }

    void Disparar()
    {
        if (!puedeDisparar) return;

        if (Input.GetKeyDown(botonDisparo) && disparar.t > disparar.tiempoCadencia && !prevenirDispararPiso.ArmaEnPiso)
        {
            disparar.Shoot();
            anim.Play(AnimacionesPlayer.disparar,1,0);
            rb2d.AddForce(-transform.right * 10, ForceMode2D.Impulse);
        }
    }

    void Moverse()
    {
        if (!puedeMoverse) return;

        if(!saltando && !grounded && !checkCayendo)
        {
            checkCayendo = true;
            StartCoroutine(CheckAterrizaje());
        }

        horizontal = Input.GetAxis("Horizontal") * velocidadMovimiento  ;
        SonidosPaso();
    }

    private void FixedUpdate()
    {
        if(disableVelocityInput)
        {
            return;
        }

        if(movilePlatformRb!= null)
        {
            rb2d.velocity = new Vector2(movilePlatformRb.velocity.x + horizontal + fuerzaExterna.x, movilePlatformRb.velocity.y) ;
        }
        else
        {
            rb2d.velocity = new Vector2(horizontal  + fuerzaExterna.x, rb2d.velocity.y);
            //rb2d.AddForce(Vector2.right * horizontal, ForceMode2D.Force);
        }
    }

    void CheckDa�oCaida()
    {
        // Si en el frame anterior no estaba en el suelo pero en este si?
        if(!prevGround && grounded)
        {
            // aterrizo
            disableVelocityInput = false;
            if(direccion.y < -velocidadDa�oCaida)
            {
                // ac� aplicar el Da�o
            }

        }
    }

    void SonidosPaso()
    {
        if(Time.time > tiempoUltimoPaso + tiempoEntrePasos && horizontal != 0 && grounded)
        {
            SoundFXManager.instance.ReproducirSFX(sonidosPasos);
            tiempoUltimoPaso = Time.time;
        }
    }


    ///////////////////////////////////////

    void CheckSuelo()
    {
        // obtener la direccion del objeto calculando la posici�n en el frame anterior y restandole la posicion actual
        direccion = (transform.position - posicionAnterior) / Time.deltaTime;
        posicionAnterior = transform.position;
        
        // esto es para saber si en el frame anterior estaba o no en el suelo
        prevGround = grounded;

        // ac� viene la magia negra
        sueloCount = 0;
        Bounds bounds = col.bounds;
        
        // abajo Izquierda
        groundCheckPos[0] = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y);

        // abajo Centro
        groundCheckPos[1] = new Vector2(bounds.center.x, bounds.center.y - bounds.extents.y);

        // abajo Derecha
        groundCheckPos[2] = new Vector2(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y);

        for (int i = 0; i < 3; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheckPos[i], Vector2.down, 0.05f, layerPiso);
            if (hit.collider != null)
                sueloCount++;
        }

        // si alguno de los 3 raycast toca suelo, entonces hay piso
        grounded = sueloCount > 0;
    }




    IEnumerator CheckAterrizaje()
    {
        yield return new WaitForSeconds(0.1f);

        while(!grounded)
        {
            yield return null;
        }

        SoundFXManager.instance.ReproducirSFX(sonidoAterrizaje);
        anim.Play(AnimacionesPlayer.aterrizar);
        saltando = false;
        checkCayendo = false;
    }



    public void AplicarRecoil(Vector2 direccion)
    {
        //rb2d.AddForce(direccion * -1, ForceMode2D.Impulse);
        fuerzaExterna = direccion * -1;
        rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y + fuerzaExterna.y);
    }

    void DisiparFuerzaExterna()
    {
        fuerzaExterna = Vector2.MoveTowards(fuerzaExterna, Vector2.zero, Time.deltaTime * disipadorFuerzaExterna);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlataformaMovil"))
        {
            // chequear si la plataformaMovil est� debajo del player

            Bounds bounds = collision.collider.bounds;
            Vector2 superficiePlataforma = new Vector2(bounds.center.x, bounds.center.y + bounds.extents.y);

            if(transform.position.y >= superficiePlataforma.y)
            {
                movilePlatformRb = collision.transform.GetComponent<Rigidbody2D>();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlataformaMovil"))
        {
            movilePlatformRb = null;
        }
    }


    void TestearFuerza()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            disableVelocityInput = true;
            
            rb2d.AddForce(Vector2.one * 10, ForceMode2D.Impulse);
        }
    }

}
