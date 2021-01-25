using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topo : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public bool faceleft;
    public int vida;
    public float tiempoInvulnerable;
    [HideInInspector]
    public int vidaInicial;
    public bool salto;
    [HideInInspector]
    public EfectosSonido playAudio;
    public bool escape;
    private bool invul;
    
    public bool quieto;
    private Rigidbody2D rb;
    private Vector2 posicionBicho;
    private Collider2D col;
    private SpriteRenderer sprite;

    [HideInInspector]
    public Animator anim;

    Color[] colores =
    {
        new Color(1f,1f,1f), //normal
        new Color (1f, 0, 0), //rojo
    };

    // Start is called before the first frame update
    public virtual void Start()

    {

        escape = false;
        playAudio = (GameObject.Find("EfectosSonidoGO")).GetComponent<EfectosSonido>();
        vidaInicial = vida;
        invul = false;
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        salto = true;
        if (!faceleft & transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
            anim.SetFloat("multiplier", speed);

    }

    // Update is called once per frame
    public virtual void Update()
    {
        anim.SetBool("saltando", salto);

        if (Input.GetKeyDown("space"))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }


        Vector2 posPantallaDerecha = Camera.main.WorldToScreenPoint(col.bounds.min);
        Vector2 posPantallaIzquierda = Camera.main.WorldToScreenPoint(col.bounds.max);
        if ((posPantallaDerecha.x> Screen.width) || (posPantallaIzquierda.x < 0))
        {
            escape = true;
        }



    }

    //FixedUpdate is called once per frame depending on delta.time
    void FixedUpdate()
    {
        if (!salto & !quieto)
        {
            rb.velocity = new Vector2(-transform.localScale.x * speed, rb.velocity.y);
        }
    }

    public void Invulnerable()
    {
        invul = true;
        InvokeRepeating("Titilar", 0f, 0.04f);
    }

    public void Vulnerable()
    {
        invul = false;
        CancelInvoke("Titilar");
        sprite.enabled = true;
    }

    private void Titilar()
    {
        if (sprite.enabled)
        {
            sprite.enabled = false;
        }
        else
        {
            sprite.enabled = true;
        }
    }

    public virtual void Golpear()
    {
        sprite.material.color = colores[1];
        playAudio.PlayGolpe();
        Invoke("Descolorear", 0.1f);
    }

    public void Descolorear()
    {
        sprite.material.color = colores[0];
    }
    public virtual void Salto()
    {
        if (this.enabled)
        {
            if ((gameObject.layer < 17) & (!invul))
            {
                Vector2 vector = new Vector2(0, 1);
                rb.velocity = new Vector2(0.0f, jumpForce);
                salto = true;
                gameObject.layer++;
                Invulnerable();
                Invoke("Vulnerable", tiempoInvulnerable);
                SetVida();
            } 
        }
    }

    public virtual void SetVida()
    {

    }


    public void EntrarRio()
    {
        quieto = true;
        anim.SetBool("muriendo", true);
        playAudio.PlayMuerte();
        Destroy(gameObject, 0.6f);
    }

    private void ContactoPlataforma()
    {
        rb.velocity = Vector2.zero;
        salto = false;
    }

    private void OnMouseDown()
    {
        if((!invul)& (this.enabled) & (gameObject.layer < 17))
        {
            Golpear(); 
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled)
        {
            if ((collision.gameObject.tag == "enemigo") & (collision.gameObject.layer == gameObject.layer))
            {
                Physics2D.IgnoreCollision(collision.collider, col, true);
            }
            if ((collision.gameObject.tag == "plataforma") & (rb.velocity.y < 1))
            {
                ContactoPlataforma();

                if ((gameObject.layer == 17) & (rb.velocity.y < 1.0f))
                {
                    EntrarRio();
                }
            } 
        }
    }
}
