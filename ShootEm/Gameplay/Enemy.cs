using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Timer shootTimer;
    Rigidbody2D rb2d;
    Vector2 forceVector;

    public void Initialize()
    {       
        rb2d = GetComponent<Rigidbody2D>();
        forceVector = new Vector2(
            GameConstants.EnemyImpulseForce, 0);

        shootTimer = gameObject.AddComponent<Timer>();
        shootTimer.Duration = GameConstants.EnemyShootDelaySeconds;
        shootTimer.AddTimerFinishedListener(HandleShootTimerFinished);
    }

    public void Activate()
    {
        // apply impulse force to get enemy moving
        rb2d.AddForce(forceVector, ForceMode2D.Impulse);

        shootTimer.Run();
    }

    public void Deactivate()
    {
        rb2d.velocity = Vector2.zero;
        shootTimer.Stop();
    }

    void OnBecameInvisible()
    {
        // don't remove when spawned
        if (transform.position.x < 0)
        {
            // return to the pool
            ObjectPool.ReturnEnemy(gameObject);
        }
    }

    void HandleShootTimerFinished()
    {
        shootTimer.Run();

        // shoot bullet
        Vector3 bulletPos = transform.position;
        bulletPos.x += GameConstants.EnemyBulletXOffset;
        bulletPos.y += GameConstants.EnemyBulletYOffset;
        GameObject bullet = ObjectPool.GetBullet();
        bullet.transform.position = bulletPos;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().StartMoving(BulletDirection.Left);
    }
}
