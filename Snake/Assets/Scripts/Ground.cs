using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    GameObject player;
    float distance;

    void Start()
    {
        player = GameObject.FindObjectOfType<Snake>().gameObject;
    }

    void Update()
    {
        distance = player.transform.position.z - gameObject.transform.position.z;
        if (distance > 300)
        {
            Destroy(gameObject);
        }
    }
}
