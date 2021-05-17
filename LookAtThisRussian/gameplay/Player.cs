using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
	CapsuleCollider[] playerCollider;
    Vector3 playerPosition;
	Rigidbody playerRigidBody;
    SpriteRenderer player;

    Timer playerColliderIsTrigger;
    Timer invulnerability;
    bool invulnerabilityIsRunning;
    
    UI scriptUI;

    float playerSpeed=10f;
    float minimumSpawnCoordinate = -445f;
    float maximumSpawnCoordinate = 445f;

    void Start()
	{
        invulnerability = gameObject.AddComponent<Timer>();
        invulnerability.Duration = 10;

        playerRigidBody = gameObject.GetComponent<Rigidbody>();

        playerCollider = gameObject.GetComponents<CapsuleCollider>();
        playerCollider[0].isTrigger = true;
        playerCollider[1].isTrigger = true;

        scriptUI = GameObject.FindObjectOfType<UI>();

        playerColliderIsTrigger = gameObject.AddComponent<Timer>();
        playerColliderIsTrigger.Duration = 1;
        playerColliderIsTrigger.Run();

        player = gameObject.GetComponentInChildren<SpriteRenderer>();

        PlayerSpawn();
    }

    private void Update()
    {
        //Was touched by a granny
        if (invulnerability.Running)
        {
            player.color = Color.red;
        }
        else
        {
            player.color = Color.white;
        }
            

        if (playerColliderIsTrigger.Finished)
        {
            playerCollider[0].isTrigger = false;
            playerCollider[1].isTrigger = false;
        }

        if (invulnerability.Finished)
        {
            invulnerabilityIsRunning = false;
        }
    }
 
    void FixedUpdate()
	{
		Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input.Normalize();
		playerRigidBody.MovePosition(transform.position+=input * Time.fixedDeltaTime * playerSpeed);
	}
   
    //Check TriggerEnter, if spawned into walls, respawn 
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Granny granny) && !invulnerabilityIsRunning)
        {
            invulnerabilityIsRunning = true;
            invulnerability.Run();
            scriptUI.HealthChange();
            FindObjectOfType<AudioManager>().Play("HealthDecreased");
        }
        else if (collision.TryGetComponent(out Vodka vodka))
        {
            return;
        }   
        else if(!invulnerabilityIsRunning) 
        {
            PlayerSpawn();
        }
    }

    void PlayerSpawn()
    {
        playerPosition = new Vector3(Random.Range(minimumSpawnCoordinate, maximumSpawnCoordinate),2, Random.Range(minimumSpawnCoordinate, maximumSpawnCoordinate));
        transform.position = playerPosition;
    }
}
