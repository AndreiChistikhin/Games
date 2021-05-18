using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text pathLengthText;

	void Start()
	{
        EventManager.AddPathFoundListener(SetPathLength);
	}

    void SetPathLength(float length)
    {
        pathLengthText.text = "Path Length: " +
            length.ToString();
    }
}
