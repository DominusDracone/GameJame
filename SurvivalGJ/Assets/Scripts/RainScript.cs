using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainScript : MonoBehaviour
{
    public float maxRazdaljina;
    private GameObject igrac;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
        igrac = GameObject.FindGameObjectWithTag("Igrac");
    }

    // Update is called once per frame
    void Update()
    {        
        
    }
}

