using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VrataScript : MonoBehaviour
{
    public GameObject igrac;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Igrac"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            igrac.SetActive(true);
            igrac.transform.position = Vector3.zero;
        }
    }
}
