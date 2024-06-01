using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float respawnDelay = 90f; // Delay before respawning the next enemy
    private bool isSpawning = false;
    private GameObject currentEnemy;
    public GameObject igrac;
    public float domet = 20f;
    private float minimumDelay = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        if (IgracUDometu()==false&&isSpawning==false)
        {
            StartCoroutine(RespawnEnemyAfterDelay());
        }
    }

    private bool IgracUDometu()
    {
        if (Math.Abs(igrac.transform.position.x - this.transform.position.x) <= domet) return true; else return false;
    }

    void SpawnEnemy()
    {
        currentEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
    }

    IEnumerator RespawnEnemyAfterDelay()
    { isSpawning = true;
        SpawnEnemy();
        yield return new WaitForSeconds(respawnDelay);
        
        if (respawnDelay > minimumDelay) { 
        respawnDelay = respawnDelay - 2f;
        }
        isSpawning = false;
    }
}
