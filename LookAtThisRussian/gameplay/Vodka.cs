using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Vodka : MonoBehaviour
{
    [SerializeField]
    GameObject pickUpBar;
    [SerializeField]
    GameObject arrow;
    Color color;

    bool collisionEnter;
    bool pickUpBarInstantiated;

    Timer vodkaAddedTimer;
    Transform player;
    UI scriptUI;
    SpriteRenderer arrowSprite;

    float minimumSpawnCoordinate = -445f;
    float maximumSpawnCoordinate = 445f;

    public bool CollisionEnter => collisionEnter;
    public Timer VodkaAddedTimer => vodkaAddedTimer;

    private void Start()
    {
        vodkaAddedTimer = gameObject.AddComponent<Timer>();

        player = GameObject.FindObjectOfType<Player>().transform;
        scriptUI = GameObject.FindObjectOfType<UI>();

        arrow = Instantiate<GameObject>(arrow, player.position, Quaternion.Euler(90, 0, 0));
        arrowSprite=arrow.GetComponent<SpriteRenderer>();
        color = arrowSprite.material.color;
    }

    void Update()
    {
        //Adjust arrows pointing at vodka
        Vector3 playerDistance = transform.position - player.position;
        Vector3 arrowPosition = player.position + (playerDistance.normalized * 50);
        arrowPosition.y = 4;
        arrow.transform.position = arrowPosition;

        float angleY = Mathf.Acos(playerDistance.x / Mathf.Sqrt(Mathf.Pow(playerDistance.x, 2) + Mathf.Pow(playerDistance.z, 2)));
        if (playerDistance.z > 0)
        {
            angleY = -angleY;
        }
        
        angleY = (angleY * 180 / Mathf.PI) + 180;
        arrow.transform.rotation = Quaternion.Euler(90, angleY, 0);

        if (collisionEnter&&!pickUpBarInstantiated)
        {
            Instantiate<GameObject>(pickUpBar, player.transform.position + new Vector3(0, 0, 7), Quaternion.Euler(90, 0, 0));
            pickUpBarInstantiated = true;
        }

        if (vodkaAddedTimer.Finished)
        {
            collisionEnter = false;
            scriptUI.TextChange();
            FindObjectOfType<AudioManager>().Play("VodkaAdded");
            Destroy(gameObject);
            Destroy(arrow);
        }

        if ((playerDistance.x > 15 || playerDistance.z > 15) && collisionEnter == true)
        {
            vodkaAddedTimer.Stop();
            collisionEnter = false;
            pickUpBarInstantiated = false;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        //Was spawned in a wall
        if(!coll.TryGetComponent(out Player player)&&!coll.TryGetComponent(out Granny granny))   
        {
            float positionX = Random.Range(minimumSpawnCoordinate, maximumSpawnCoordinate);
            float positionY = 2;
            float positionZ = Random.Range(minimumSpawnCoordinate, maximumSpawnCoordinate);
            Instantiate(gameObject, new Vector3(positionX, positionY, positionZ), Quaternion.Euler(90, 0, 0));
            Destroy(gameObject);
            Destroy(arrow);
        }

        else if(coll.TryGetComponent(out Player player1))
        {
            if (collisionEnter == false)
            {
                vodkaAddedTimer.Duration = 10;
                vodkaAddedTimer.Run();
                collisionEnter = true;
            }
        }
    }
 
    //Hide or show arrows to Vodka
    private void OnBecameVisible()
    {
        color.a = 0f;
        arrowSprite.material.color = color;
    }

    private void OnBecameInvisible()
    {
        color.a = 1f;
        arrowSprite.material.color = color;
    }
}

