using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtThisRussian : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
                
        }
    }
}
