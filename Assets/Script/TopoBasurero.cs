using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopoBasurero : Topo
{
    public float tiempoMinimo;
    public float tiempoMaximo;
    private float tiempo;
    public float tiempoQuieto;
    private TopoCacerola comportamiento2;

    // Start is called before the first frame update
    public override void Start()
    {

        base.Start();
        comportamiento2 = GetComponent<TopoCacerola>();
        tiempo = Random.Range(tiempoMinimo, tiempoMaximo);
        if (!quieto)
        {
            Moverse();
        }
    }

    public override void Update()
    {
        base.Update();
    }
    public void Quedarse()
    {
            quieto = true;
            anim.SetFloat("multiplier", 0f);
            Vulnerable();
            Invoke("Moverse", tiempoQuieto);
    }

    public void Moverse()
    {
        if (this.enabled)
        {
            quieto = false;
            anim.SetFloat("multiplier", speed);
            Invulnerable();
            tiempo = Random.Range(tiempoMinimo, tiempoMaximo);
            Invoke("Quedarse", tiempo); 
        }
    }

    public override void Golpear()
    {
        base.Golpear();
        vida--;
        if (vida < 1)
        {
            Salto();
        }
    }

    public override void Salto()
    {
        base.Salto();
        transform.localScale= new Vector3(transform.localScale.x, 1, transform.localScale.z);

        Invoke("CambiarComportamiento", tiempoInvulnerable);

    }

    private void CambiarComportamiento()
    {
        CancelInvoke("Moverse");

        comportamiento2.enabled = true;
        comportamiento2.faceleft = this.faceleft;
        this.enabled = false;
    }
}
