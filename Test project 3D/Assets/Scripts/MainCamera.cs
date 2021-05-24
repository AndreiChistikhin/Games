using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    GameObject player;
    Vector3 position;

    void Start()
    {
        player = GameObject.FindObjectOfType<Snake>().gameObject;   
    }

    void Update()
    {
        gameObject.transform.position = player.transform.position + new Vector3(-player.transform.position.x, 40, -35f);
    }
}
