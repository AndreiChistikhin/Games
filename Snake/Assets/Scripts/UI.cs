using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    Text peopleEatenCount;
    [SerializeField]
    Text peopleEatenCount2;
    int peopleEatenNumber;

    [SerializeField]
    Text diamondCount;
    public int diamondNumber { get; set; }

    public void PeopleEaten() 
    {
        peopleEatenNumber++;
        peopleEatenCount.text = peopleEatenNumber.ToString();
        peopleEatenCount2.text = peopleEatenNumber.ToString();
    }
    public void DiamondAdded()
    {
        diamondNumber++;
        
    }
    private void Update()
    {
        diamondCount.text = diamondNumber.ToString();
    }

    public void PlayAgainButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
