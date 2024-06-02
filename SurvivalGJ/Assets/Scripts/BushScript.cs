using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BushScript : MonoBehaviour
{
    public Sprite originalnoStanje;
    public Sprite promenjenoStanje;

    private GameObject igrac;    
    // Start is called before the first frame update
    void Start()
    {
        igrac = GameObject.FindGameObjectWithTag("Igrac");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Igrac"))
        {
            PlayerMovement pm = igrac.GetComponent<PlayerMovement>();
            pm.nextToBush = true;
            Debug.Log("Next to bush");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Igrac"))
        {
            PlayerMovement pm = igrac.GetComponent<PlayerMovement>();
            pm.nextToBush = false;
            Debug.Log("Not next to bush");
        }
    }

    internal void SakrijiIgraca()
    {
        GetComponent<SpriteRenderer>().sprite = promenjenoStanje;
    }

    internal void IgracJeIzasao()
    {
        GetComponent<SpriteRenderer>().sprite = originalnoStanje;
    }
}
