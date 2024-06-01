using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingScript : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;

    [SerializeField] private Rigidbody2D rb;

    //chcek if something colided with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        //wait for a short time
        yield return new WaitForSeconds(fallDelay);
        //then set to rigit body tipe to dynamic
        rb.bodyType = RigidbodyType2D.Dynamic;
        //we destroy after a short amount of time
        Destroy(gameObject, destroyDelay);
    }
}
