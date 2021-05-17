using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{
    [SerializeField]
    RawImage[] life;
    int lifeNumber;

    [SerializeField]
    Text vodkaText;
    int vodkaPoints;
    int vodkaNumber = 10;

    [SerializeField]
    Text grannyText;
    int grannyCount;
    int grannyMaxNumber = 7;
    

    private void Start()
    {
        lifeNumber = life.Length;
    }

    //StartGameAgain
    private void Update()
    {
        if (lifeNumber == 0 || vodkaPoints == 10)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    
    public void TextChange()
    {
        vodkaPoints++;
        vodkaText.text = vodkaPoints + "/"+vodkaNumber;
    }

    public void HealthChange()
    {
        lifeNumber--;
        Destroy(life[lifeNumber]);
    }
    
    public void GrannyAdded()
    {
        grannyCount++;
        grannyText.text = grannyCount + "/"+grannyMaxNumber;
    }
}
