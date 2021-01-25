using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopoGigante : Topo
{

    public override void Golpear()
    {
        base.Golpear();
        vida--;
        if (vida < 1)
        {
            Salto();
        }
    }

    public override void SetVida()
    {
        vida = vidaInicial;
    }
}
