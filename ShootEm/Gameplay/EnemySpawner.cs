using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An enemy spawner
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    Timer spawnTimer;

    float verticalBorderSize;
    float horizontalOffset;

	void Start()
	{
        GameObject enemy = ObjectPool.GetEnemy();
        Collider2D collider = enemy.GetComponent<PolygonCollider2D>();
        horizontalOffset = collider.bounds.size.x;
        verticalBorderSize = collider.bounds.size.y * 4;
        ObjectPool.ReturnEnemy(enemy);

        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = GameConstants.EnemySpawnDelaySeconds;
        spawnTimer.AddTimerFinishedListener(HandleSpawnTimerFinished);
        spawnTimer.Run();
	}

    void HandleSpawnTimerFinished()
    {
        SpawnEnemy();
        spawnTimer.Run();
    }

    public void Stop()
    {
        spawnTimer.Stop();
    }

    void SpawnEnemy()
    {
        // get random position
        Vector3 enemyPos = new Vector3(
            ScreenUtils.ScreenRight + horizontalOffset,
            Random.Range(ScreenUtils.ScreenTop - verticalBorderSize,
                ScreenUtils.ScreenBottom + verticalBorderSize),
            0);

        // spawn enemy object
        GameObject enemy = ObjectPool.GetEnemy();
        enemy.transform.position = enemyPos;
        enemy.SetActive(true);

        // kluge because some enemies are going faster when spawned
        enemy.GetComponent<Enemy>().Deactivate();

        enemy.GetComponent<Enemy>().Activate();
    }
}
