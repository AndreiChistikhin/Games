using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Vector3 tailTarget;
    public GameObject tailTargetObject;
    List<GameObject> snakeCount;
    Color tailColor;
    Snake snakeScript;
    
    private void Start()
    {
        snakeScript = GameObject.FindObjectOfType<Snake>();
        snakeCount = snakeScript.TailObjects;
        tailTargetObject = snakeCount[snakeCount.Count - 2];

        tailColor = gameObject.GetComponent<Renderer>().material.color;
    }
    private void FixedUpdate()
    {
        tailTarget = tailTargetObject.transform.position;
        transform.LookAt(tailTarget);
        transform.position = Vector3.Lerp(transform.position, tailTarget, 0.2f);

        tailColor = snakeScript.gameObject.GetComponent<Renderer>().material.color;
        gameObject.GetComponent<Renderer>().material.color = tailColor;
    }
}
