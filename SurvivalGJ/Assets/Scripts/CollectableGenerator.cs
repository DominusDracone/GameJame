using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGenerator : MonoBehaviour
{
    public GameObject grancica; // The apple prefab to spawn
    public GameObject bobica; // The orange prefab to spawn
    public float spawnInterval = 5f; // Interval in seconds between spawns

    private GameObject currentCollectible;

    void Start()
    {
        StartCoroutine(SpawnCollectibleRoutine());
    }

    IEnumerator SpawnCollectibleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnCollectible();
        }
    }

    void SpawnCollectible()
    {
        if (currentCollectible != null) return;

        int randomChoice = Random.Range(0, 2);
        if (randomChoice == 0)
        {
            currentCollectible = Instantiate(grancica, transform.position, transform.rotation);
        }
        else
        {
            currentCollectible = Instantiate(bobica, transform.position, transform.rotation);
        }
    }

    public void CollectibleCollected()
    {
        currentCollectible = null;
    }
}
