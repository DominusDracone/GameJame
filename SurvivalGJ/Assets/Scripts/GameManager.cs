using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }        
    }

    public AudioSource bg1;
    public AudioSource bg2;
    public AudioSource bg3;
    public AudioSource asSFX;
    public List<AudioClip> SFXs;

    private PlayerLifeHP playerLife;
    private AudioSource currentAudio;
    private bool isTransitioning=false;
    // Start is called before the first frame update
    void Start()
    {
        playerLife= GameObject.Find("Player").GetComponent<PlayerLifeHP>();
        if (bg1 == null || bg2 == null || bg3 == null) {
            throw new UnityException("BG muzika ne sme biti null.");

        }
        if (playerLife==null)
        {
            throw new UnityException("Player Life is null.");

        }
        currentAudio = bg1;
        currentAudio.volume = 1;
        bg2.volume = 0;
        bg3.volume=0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransitioning == false) { 
        if (playerLife.currentHealth < 33 && currentAudio != bg3) { StartCoroutine(TransitionMusic(3)); }
        
        else if (playerLife.currentHealth > 66 && currentAudio != bg1) { StartCoroutine(TransitionMusic(1)); } 
        
        else if (playerLife.currentHealth >=33&& playerLife.currentHealth <=66&& currentAudio != bg2) { StartCoroutine(TransitionMusic(2)); }}

        

    }

    IEnumerator TransitionMusic(int i)
    { isTransitioning= true;

        switch (i)
        {
            case 1:
                while (currentAudio.volume != 0)
                {
                    currentAudio.volume -= 0.05f;
                    bg1.volume += 0.1f;

                    yield return new WaitForSeconds(0.05f);
                }

                currentAudio.volume = 0;
                bg1.volume = 1;


                currentAudio = bg1;

                break;
                case 2:
                while (currentAudio.volume != 0)
                {
                    currentAudio.volume -= 0.05f;
                    bg2.volume += 0.1f;

                    yield return new WaitForSeconds(0.05f);
                }

                currentAudio.volume = 0;
                bg2.volume = 1;


                currentAudio = bg2;
                break;
                case 3:
                while (currentAudio.volume != 0)
                {
                    currentAudio.volume -= 0.05f;
                    bg3.volume += 0.1f;

                    yield return new WaitForSeconds(0.05f);
                }

                currentAudio.volume = 0;
                bg3.volume = 1;


                currentAudio = bg3;
                break;
        }

   
        isTransitioning = false;
    }

    internal void PustiZvuk(string ime)
    {        
        foreach (AudioClip ac in SFXs)
        {
            if (ac.name == ime)
            {
                asSFX.clip = ac;
                asSFX.Play();
                break; 
            }
        }
    }
}
