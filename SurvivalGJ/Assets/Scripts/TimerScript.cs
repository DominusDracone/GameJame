using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class TimerCript : MonoBehaviour
{
    public TextMeshProUGUI textMesh = null;
    public GameObject vrata;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);
        textMesh.text = string.Format("{0:00}:{1:00}", min, sec);
    }

    private void OtvoriNoviNivo()
    {
       vrata.SetActive(true);
    }

    internal void Restart()
    {
        timer = 0;
    }
}
