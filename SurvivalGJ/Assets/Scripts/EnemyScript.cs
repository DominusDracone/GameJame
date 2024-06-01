using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float brzina;
    public GameObject stake;

    private Collider2D pogled;
    private bool vidiIgraca = false;
    private Rigidbody2D rb;
    private Vector2 pocetnaPozicija;
    private Smer smer = Smer.desno;
    private Transform igracPozicija;
    private GameObject igrac;

    // Start is called before the first frame update
    void Start()
    {
        igrac = GameObject.FindGameObjectWithTag("Igrac");
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Udarac");
        if (collision.collider.CompareTag("Zamka"))
        {
            UnistiSe(collision.collider);
        }
        if (collision.collider.CompareTag("Igrac"))
        {
            Ujedi();
        }
    }

    private void Ujedi()
    {
        Debug.Log("Ujeo sam ga");
    }

    private void UnistiSe(Collider2D collider)
    {
        Debug.Log("Unistio sam se");
        IspustiResurse();
        Destroy(collider.gameObject);//unistavanje zamke
        Destroy(gameObject);//unistavanje sebe
    }

    private void IspustiResurse()
    {
        Instantiate(stake, transform.position, new Quaternion());
    }
}