using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusica : MonoBehaviour
{
    
    public List<AudioClip> songList;
    [SerializeField]
    [Range(1f, 100f)]
    public List<int> listaProb;
    public List<float> listaProbReal;
    private AudioSource playAudio;
    // Start is called before the first frame update
    void Start()
    {
        playAudio = GetComponent<AudioSource>();
        int i = PosicionDeProbabilidad();
        playAudio.clip = songList[i];
        playAudio.loop = true;
        playAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Le re copiaba del otro viste jaja re caro el dolar.
    private void OnValidate()
    {
        if (songList.Count != listaProb.Count)
        {
            Resize(listaProb, songList.Count);
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


    //le copiaba otra vez na que ver jaja a nisman lo mataron


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
