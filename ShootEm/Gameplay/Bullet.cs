using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 forceVector;

    public Vector2 ForceVector=>forceVector;

    public void Initialize()
    {       
        rb2d = GetComponent<Rigidbody2D>();
        forceVector = new Vector2(
            GameConstants.BulletImpulseForce, 0);
    }

    public void StartMoving(BulletDirection direction)
    {
        // apply impulse force to get projectile moving
        if (direction == BulletDirection.Left)
        {
            forceVector.x = -GameConstants.BulletImpulseForce;
        }
        else
        {
            forceVector.x = GameConstants.BulletImpulseForce;
        }
        rb2d.AddForce(forceVector, ForceMode2D.Impulse);
    }

    public void StopMoving()
    {
        rb2d.velocity = Vector2.zero;
    }

    void Update()
    {
        if (gameObject.activeInHierarchy &&
            rb2d.velocity.magnitude < 0.1f)
        {
            ObjectPool.ReturnBullet(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // return to the pool
        ObjectPool.ReturnBullet(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if colliding with a bullet, return both to pool
        if (other.gameObject.CompareTag("Bullet"))
        {
            ObjectPool.ReturnBullet(other.gameObject);
            ObjectPool.ReturnBullet(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            // if colliding with enemy return both to 
            // their respective pools
            ObjectPool.ReturnEnemy(other.gameObject);
            ObjectPool.ReturnBullet(gameObject);
        }
    }
}
