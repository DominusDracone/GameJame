using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectDestroyObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            collision.gameObject.SetActive(false);
            
        }

    }
}
