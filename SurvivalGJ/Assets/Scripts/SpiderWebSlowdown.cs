using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWebSlowdown : MonoBehaviour
{
    [SerializeField]public float slowdownFactor = 0.5f; // Adjust the slowdown factor as needed



    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Apply slowdown directly through the player movement script
                playerMovement.ApplySpiderWebSlowdown(slowdownFactor);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Reset slowdown factor when player exits the spider web
                playerMovement.ResetSpiderWebSlowdown();
            }
        }
    }
}
