using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The player ship
/// </summary>
public class Ship : MonoBehaviour
{
    int health = 100;

    bool previousFrameShootInput = false;
    float colliderHalfHeight;
    HealthChanged healthChangedEvent = new HealthChanged();
    GameOver gameOverEvent = new GameOver();

	void Start()
	{
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        colliderHalfHeight = collider.size.y / 2;

        EventManager.AddHealthChangedInvoker(this);
        EventManager.AddGameOverInvoker(this);
    }
	
	void Update()
	{
        // move based on input
        Vector3 position = transform.position;
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0)
        {
            position.y += verticalInput * 
                GameConstants.ShipMoveUnitsPerSecond *
                Time.deltaTime;
        }

        // move character to new position and clamp in screen
        transform.position = position;
        ClampInScreen();

        // check for shooting input
        if (Input.GetAxis("Shoot") > 0)
        {
            // only shoot on first input frame
            if (!previousFrameShootInput)
            {
                previousFrameShootInput = true;

                // shoot bullet
                Vector3 bulletPos = transform.position;
                bulletPos.x += GameConstants.ShipBulletOffset;
                GameObject bullet = ObjectPool.GetBullet();
                bullet.transform.position = bulletPos;
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().StartMoving(BulletDirection.Right);
            }
        }
        else
        {
            // no shoot input
            previousFrameShootInput = false;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            ObjectPool.ReturnBullet(other.gameObject);
            TakeDamage(GameConstants.ShipBulletCollisionDamage);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {

            ObjectPool.ReturnEnemy(other.gameObject);
            TakeDamage(GameConstants.ShipEnemyCollisionDamage);
        }
    }

    void ClampInScreen()
    {
        Vector3 position = transform.position;
        if (position.y + colliderHalfHeight > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenTop - colliderHalfHeight;
        }
        else if (position.y - colliderHalfHeight < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenBottom + colliderHalfHeight;
        }
        transform.position = position;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            healthChangedEvent.Invoke(health);
        }
        else
        {
            gameOverEvent.Invoke();
        }
    }

    public void AddHealthChangedListener(UnityAction<int> listener)
    {
        healthChangedEvent.AddListener(listener);
    }

    public void AddGameOverListener(UnityAction listener)
    {
        gameOverEvent.AddListener(listener);
    }
}
