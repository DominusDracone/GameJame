using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollows : MonoBehaviour
{

    [SerializeField] private Transform player;
    void Update()
    {
        //transform.position = new Vector3(player.position.x, player.position.y +2, transform.position.z);
        transform.position = new Vector3(player.position.x, player.position.y + 2, transform.position.z);
    }
}
