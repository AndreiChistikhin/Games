using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    [SerializeField]
    Text healthText;

	void Awake()
	{
        EventManager.AddHealthChangedListener(HandleHealthChangedEvent);
	}

    void HandleHealthChangedEvent(int health)
    {
        healthText.text = "Health: " + health.ToString();
    }
}
