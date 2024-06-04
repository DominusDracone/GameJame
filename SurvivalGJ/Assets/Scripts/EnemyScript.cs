using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float brzina;
    public GameObject stake;
    public float domet;

    private Collider2D pogled;
    private bool vidiIgraca = false;
    private Rigidbody2D rb;
    private Vector2 pocetnaPozicija;
    private Smer smer = Smer.levo;
    private Transform igracPozicija;
    private GameObject igrac;
    private bool ujeo;
    private GameObject[] zbunje;    

    // Start is called before the first frame update
    void Start()
    {
        zbunje = GameObject.FindGameObjectsWithTag("Bush");
        igrac = GameObject.FindGameObjectWithTag("Igrac");
        pogled = transform.GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        pocetnaPozicija = transform.localPosition;
        igracPozicija = igrac.transform;

        foreach(GameObject go in zbunje)
        {
            Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), go.GetComponent<Collider2D>(), true);
        }
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
                if (transform.position.x < pocetnaPozicija.x + domet)
                {
                    rb.velocity = Vector2.right;
                }
                else
                {
                    smer = Smer.levo;
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                break;
            case Smer.levo:
                if (transform.position.x > pocetnaPozicija.x - domet)
                {
                    rb.velocity = Vector2.left;
                }
                else
                {
                    smer = Smer.desno;
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                break;
        }          
    }

    private void Napadni()
    {

        float razdaljina = igracPozicija.position.x - transform.position.x;
        if (razdaljina > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        rb.velocity = new Vector2(razdaljina, 0) * brzina;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement pm = igrac.GetComponent<PlayerMovement>();
        if (collision.CompareTag("Igrac") && !pm.isHidden)
        {
            vidiIgraca = true;
            pm.isHunted = true;
            GameManager.Instance.PustiZvuk("bugsfx1");
            Debug.Log("vidim ga sada");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Igrac"))
        {
            vidiIgraca = false;
            igrac.GetComponent<PlayerMovement>().isHunted = false;
            Debug.Log("Ne vidim ga vise");
            if (smer.Equals(Smer.levo))
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Zamka":
                GameManager.Instance.PustiZvuk("normaltrapactivatedsfx");
                UnistiSe(collision.collider.gameObject);
                break;
            case "Enemy":
                PromeniSmer();
                break;
            case "Zid":
                PromeniSmer();
                break;
            case "Igrac":
                rb.velocity = Vector2.zero;
                Ujedi(collision.collider);
                break;
            case "Explozija":
                EksplozivnaZamka ez = collision.collider.GetComponent<EksplozivnaZamka>();
                GameManager.Instance.PustiZvuk("explosionsfx_final");
                ez.Explosion(transform.gameObject);
                break;
        }
        Debug.Log("Udarac");

    }

    private void PromeniSmer()
    {
        if(smer == Smer.desno)
        {
            smer = Smer.levo;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            smer = Smer.desno;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Ujedi(Collider2D collider)
    {
        Debug.Log("Ujeo sam ga");
        GameManager.Instance.PustiZvuk("playerhurtsfx");
        PlayerLifeHP plHP = collider.GetComponent<PlayerLifeHP>();
        plHP.currentHealth -= 30;
        PlayerMovement pm = collider.GetComponent<PlayerMovement>();
        pm.Odgurni(transform.position);
    }

    public void UnistiSe(GameObject go)
    {
        Debug.Log("Unistio sam se");
        IspustiResurse();
        Destroy(go);//unistavanje zamke
        Destroy(gameObject);//unistavanje sebe
    }

    private void IspustiResurse()
    {
        Instantiate(stake, transform.position, new Quaternion());
    }

}
