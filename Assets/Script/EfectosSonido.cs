using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectosSonido : MonoBehaviour
{

    public AudioClip golpe;
    public AudioClip muerte;
    private AudioSource audioComp;
    
    // Start is called before the first frame update
    void Start()
    {
        audioComp = GetComponent<AudioSource>();
    }

    public void PlayGolpe()
    {
        audioComp.clip = golpe;
        audioComp.Play();
    }

    public void PlayMuerte()
    {
        audioComp.clip = muerte;
        audioComp.Play();
    }
}
