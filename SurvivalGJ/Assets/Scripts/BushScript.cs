using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BushScript : MonoBehaviour
{
    private GameObject igrac;
    // Start is called before the first frame update
    void Start()
    {
        igrac = GameObject.FindGameObjectWithTag("Igrac");
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), igrac.GetComponent<Collider2D>(), true);
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Igrac"))
        {
            PlayerMovement pm = igrac.GetComponent<PlayerMovement>();
            pm.nextToBush = false;
        }
    }
}
