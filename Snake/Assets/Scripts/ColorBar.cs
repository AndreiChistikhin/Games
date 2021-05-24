using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorBar : MonoBehaviour
{
    Renderer colorBarRenderer;
    void Start()
    {
        colorBarRenderer = gameObject.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Snake snake))
        {
            other.GetComponent<Renderer>().material.color=colorBarRenderer.material.color;
        }
    }
}
