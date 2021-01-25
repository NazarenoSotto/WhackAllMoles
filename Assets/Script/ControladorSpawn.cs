using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSpawn : MonoBehaviour
{
    public GameObject piso1;
    public GameObject piso2;
    public GameObject piso3;
    public float radiomin;
    public float radiomax;

    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn(GameObject topo)
    {

        Topo comportamiento = topo.GetComponent<Topo>();
        int caso = Random.Range(1, 4);
        float componenteY = 0f;
        float componenteZ = 0f;
        switch (caso)
        {
            case 1:
                componenteY = piso1.transform.position.y + 2;
                topo.layer = 14;
                componenteZ = 0f;
                break;
            case 2:
                componenteY = piso2.transform.position.y + 2;
                topo.layer = 15;
                componenteZ = 1f;
                break;
            case 3:
                componenteY = piso3.transform.position.y + 2;
                topo.layer = 16;
                componenteZ = 2f;

                break;
        }

        float componenteX = Random.Range(-radiomax, radiomax);
        bool mira;

        if ((componenteX < radiomin) & (componenteX > -radiomin))
        {
            mira = Random.Range(0, 2) > 0;
        }
        else
        {
            if (componenteX <= -radiomin)
            {
                mira = true;
            }

            else
            {
                mira = false;
            }
        }

        Vector3 coord = new Vector3(componenteX, componenteY, componenteZ);


        comportamiento.faceleft = mira;
        topo.transform.position = coord;
        Instantiate(topo, coord, Quaternion.identity);
    }



}
