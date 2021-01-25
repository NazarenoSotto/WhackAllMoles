using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int contadorVida;
    public GameObject mostradorDeTexto;
    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = mostradorDeTexto.GetComponent<TextMeshProUGUI>();
        contadorVida = 3;
        textMesh.text = "Vidas: " + contadorVida;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if ((col.gameObject.tag == "enemigo") && (col.GetComponent<Topo>() != null))
        {
            Topo topoActual = col.GetComponent<Topo>();
            if (topoActual.escape)
            {
                Destroy(col.gameObject);
                contadorVida--;
                textMesh.text = "Vidas: " + contadorVida;
            }

        }
    }
}
