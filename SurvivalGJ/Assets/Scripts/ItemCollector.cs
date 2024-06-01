using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int bubbles = 0;
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble")){
            Destroy(collision.gameObject);
            bubbles++;
            Debug.Log("Bubbles: " + bubbles);
        }
           
    }
}
