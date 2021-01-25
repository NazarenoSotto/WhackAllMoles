using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Oleada : MonoBehaviour
{

    public float rateInicial;
    public float rateFinal;
    public int cambiosDeRate;
    public float duracionDelEscalado;
    public string textoDeFinal;

    public List<GameObject> listaTopos;
    public List<int> listaCant;
    [SerializeField]
    [Range(1f, 100f)]
    public List<int> listaProb;
    public List<float> listaProbReal;
    public GameObject controladorSpawn;
    public GameObject mostradorDeTexto;
    private TextMeshProUGUI textMesh;
    private ControladorSpawn controlSpawn;

    private float tiempoDeCambio;
    private float rateScale;
    private float rate;
    private bool terminando;
    

    // Start is called before the first frame update
    void Start()
    {
        terminando = false;
        textMesh = mostradorDeTexto.GetComponent<TextMeshProUGUI>();
        textMesh.text = textoDeFinal;
        textMesh.enabled = false;
        rate = rateInicial;
        rateScale = (rateInicial - rateFinal) / cambiosDeRate;
        tiempoDeCambio = duracionDelEscalado / cambiosDeRate;

        controlSpawn = controladorSpawn.GetComponent<ControladorSpawn>();
        Invoke("Spawn", rate);
        Invoke("CambiarRate", tiempoDeCambio);
    }

    // Update is called once per frame
    void Update()
    {
        if ((terminando) & (GameObject.FindWithTag("enemigo") == null))
        {
            textMesh.enabled = true;
        }
    }


    private void Spawn()
    {
        if (CantidadTotal() > 1)
        {
            SpawnTopo();
            Invoke("Spawn", rate);
        }
        else
        {
            SpawnTopo();
            terminando = true;
        }
        
    }

    private void CambiarRate()
    {
        if (rate > rateFinal)
        {
            rate = rate - rateScale;
            Invoke("CambiarRate", tiempoDeCambio);
        }

    }


    private void OnValidate()
    {
        if ((listaTopos.Count != listaCant.Count) | (listaTopos.Count != listaProb.Count))
        {
            Resize(listaCant, listaTopos.Count);
            Resize(listaProb, listaTopos.Count);
        }

        listaProbReal = new List<float>();
        int i = 0;
        foreach (var item in listaProb)
        {
            listaProbReal.Add((float)item / PorcentajeTotal());
            i++;
        }
    }

    private float PorcentajeTotal()
    {
        float total = 0f;
        foreach (var item in listaProb)
        {
            total += item;
        }
        return total;
    }

    private int PosicionDeProbabilidad()
    {
        float num = Random.value;
        float suma = 0;
        int i = 0;
        do
        {
            suma += listaProbReal[i];
            i++;
        } while (suma < num);
        i--;
        return i;
    }

    private void SpawnTopo()
    {
        int i = 0;
        if (CantidadTotal() > 0)
        {
            do
            {
                i = PosicionDeProbabilidad();
            } while (listaCant[i] == 0);

            listaCant[i]--;
            Debug.LogFormat("topo seleccionado: {0}", listaTopos[i]);
            controlSpawn.Spawn(listaTopos[i]);
            Debug.LogFormat("Restantes: {0}", CantidadTotal());
        }
        else
        {
            Debug.LogFormat("Lista vacía man");
        }
    }

    private int CantidadTotal()
    {
        int total = 0;
        foreach (var b in listaCant)
        {
            total += b;
        }
        return total;
    }

    //esto cambia el tamaño de las listas
    public static void Resize(List<int> list, int newCount)
    {
        if (newCount <= 0)
        {
            list.Clear();
        }
        else
        {
            while (list.Count > newCount) list.RemoveAt(list.Count - 1);
            while (list.Count < newCount) list.Add(1);
        }
    }


}
