using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZamkaScript : MonoBehaviour
{
    private GameObject igrac;

    // Start is called before the first frame update
    void Start()
    {
        igrac = GameObject.FindGameObjectWithTag("Igrac");
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), igrac.GetComponent<Collider2D > (), true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
