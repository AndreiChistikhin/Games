using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VodkaSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject vodka;
    
    void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            float positionX = Random.Range(-445, 445);
            float positionY = 2;
            float positionZ = Random.Range(-445, 445);
            Instantiate(vodka, new Vector3(positionX, positionY, positionZ), Quaternion.Euler(90, 0, 0));
        } 
    }
}
