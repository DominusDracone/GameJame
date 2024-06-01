using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCript : MonoBehaviour
{
    public TextMeshProUGUI textMesh = null;
    private float time = 0;
    private int seconds = 0;
    private int minutes = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.realtimeSinceStartup;
        minutes = (int)(time / 60f);
        seconds = (int)time % 60;

        // Debug.Log("minutes:" + minutes + "seconds:" + seconds);
        textMesh.text = minutes + ":" + seconds;
    }
}
