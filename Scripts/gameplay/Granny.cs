using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Granny : MonoBehaviour
{
    NavMeshAgent babka;
    Transform target; 
    Vector3 rotation;
   
    void Start()
    {
        babka = GetComponent<NavMeshAgent>();
        target = GameObject.FindObjectOfType<Player>().transform;
        rotation = gameObject.transform.eulerAngles;
        rotation.x = 90;
    }

    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(rotation.x, 0, 0);
        babka.SetDestination(target.position);
    }

}
