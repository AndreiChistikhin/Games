using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Vector3 cameraPosition;
    Vector3 playerPosition;

    void Update()
    {
        cameraPosition = transform.position;
        playerPosition = GameObject.FindObjectOfType<Player>().transform.position;
        cameraPosition.x = playerPosition.x;
        cameraPosition.z = playerPosition.z;
        transform.position = cameraPosition;  
    }

}
