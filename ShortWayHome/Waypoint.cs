using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    int id;

    void OnTriggerEnter2D(Collider2D other)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;
    }

    public Vector2 Position
    {
        get { return transform.position; }
    }

    public int Id
    {
        get { return id; }
    }
}
