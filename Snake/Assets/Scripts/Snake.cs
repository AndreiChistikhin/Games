using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
    [SerializeField]
    GameObject tailPrefab;
    [SerializeField]
    GameObject finishMenu;
    [SerializeField]
    GameObject feverUI;

    Rigidbody rb;
    Renderer snakeRenderer;
    List<GameObject> tailObjects;
    float zOffset = -0.5f;
    float snakeSpeed=70f;

    int peopleCount=0;
    UI scriptUI;

    bool tailIsAdded;
    bool feverIsActive;
    Timer feverTimer;
   
    public UnityEvent onPeopleKilledEvent;
    public UnityEvent onDiamondAddedEvent;

    public List<GameObject> TailObjects => tailObjects; 

    void Start()
    {
        feverTimer = gameObject.AddComponent<Timer>();
        feverTimer.Duration = 5;

        scriptUI = GameObject.FindObjectOfType<UI>();

        tailObjects = new List<GameObject>();
        tailObjects.Add(gameObject);

        snakeRenderer = gameObject.GetComponent<Renderer>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput;
        float headRotation;

        if (feverIsActive == false)
        {
            int i = 0;
            if (i < Input.touchCount)
            {
                Vector3 gameObjectPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                if (gameObjectPosition.x > Input.GetTouch(i).position.x)
                {
                    horizontalInput = -1f;
                    headRotation = -45;
                }
                else 
                {
                    horizontalInput = 1f;
                    headRotation = 45;
                }

            }

            else
            {
                horizontalInput = 0;
                headRotation = 0;
            }

            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, headRotation, gameObject.transform.rotation.z);
            rb.MovePosition(gameObject.transform.position + new Vector3(horizontalInput, 0, 1) * snakeSpeed * Time.fixedDeltaTime);
        }

        else 
        {
            feverUI.SetActive(true);
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, 0, gameObject.transform.rotation.z);
            gameObject.transform.position=new Vector3(0,gameObject.transform.position.y,gameObject.transform.position.z);
            transform.Translate(new Vector3(0, 0, 1) * snakeSpeed*3 * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        if (peopleCount % 5 == 0&& peopleCount !=0&&!tailIsAdded)
        {
            AddTail();
            tailIsAdded = true;
        }

        if (peopleCount % 5 != 0)
        {
            tailIsAdded = false;
        }
            
        if (scriptUI.diamondNumber == 3)
        {
            feverTimer.Run();
            feverIsActive = true;
            scriptUI.diamondNumber = 0;
        }

        if (feverTimer.Finished)
        {
            feverUI.SetActive(false);
            feverIsActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<People>(out People people))
        {
            if (other.gameObject.GetComponent<Renderer>().material.color == snakeRenderer.material.color)
            {
                peopleCount++;
                onPeopleKilledEvent.Invoke();
            }

            else
            {
                WrongCollision();
            }
        }

        else if (other.TryGetComponent<Bomb>(out Bomb bomb))
        {
            WrongCollision();
        }

        else if (other.TryGetComponent<Diamond>(out Diamond diamond))
        {
            onDiamondAddedEvent.Invoke();
        }

        if (!other.gameObject.TryGetComponent(out Ground ground) && !other.gameObject.TryGetComponent(out LastGround lastGround))
        {
            other.gameObject.SetActive(false);
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<LastGround>(out LastGround groud))
        {
            finishMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void AddTail() 
    {
        Vector3 newTailPos = tailObjects[tailObjects.Count-1].transform.position;
        newTailPos.z -= zOffset;
        tailObjects.Add(Instantiate(tailPrefab, newTailPos, Quaternion.identity));
    }


    void WrongCollision()
    {
        if (feverIsActive == false)
            SceneManager.LoadScene("SampleScene");
        else
            return;
    }

    
}
