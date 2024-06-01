using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject igrac;
    public float brzina;

    private Collider2D pogled;
    private bool vidiIgraca = false;
    private Rigidbody2D rb;
    private Vector2 pocetnaPozicija;
    private Smer smer = Smer.desno;
    private Transform igracPozicija;
    // Start is called before the first frame update
    void Start()
    {
        pogled = transform.GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        pocetnaPozicija = transform.localPosition;
        igracPozicija = igrac.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (vidiIgraca)
        {
            Napadni();
        }
        else
        {
            Roam();
        }
    }

    private void Roam()
    {
        switch (smer)
        {
            case Smer.desno:
                if (transform.position.x < pocetnaPozicija.x + 5)
                {
                    rb.velocity = Vector2.right;
                }
                else
                {
                    smer = Smer.levo;
                }
                break;
            case Smer.levo:
                if (transform.position.x > pocetnaPozicija.x - 5)
                {
                    rb.velocity = Vector2.left;
                }
                else
                {
                    smer = Smer.desno;
                }
                break;
        }          
    }

    private void Napadni()
    {
        rb.velocity = (igracPozicija.position - transform.position) * brzina;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Igrac"))
        {
            vidiIgraca = true;
            Debug.Log("vidim ga sada");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Igrac"))
        {
            vidiIgraca = false;
            Debug.Log("Ne vidim ga vise");
        }
    }
}
