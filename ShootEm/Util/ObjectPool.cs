using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPool : MonoBehaviour
{
    static GameObject prefabBullet;
    static GameObject prefabEnemy;
    static Dictionary<PooledObjectName, List<GameObject>> pools;

    public static void Initialize()
    {
        prefabBullet = Resources.Load<GameObject>("Bullet");
        prefabEnemy = Resources.Load<GameObject>("Enemy");

        pools = new Dictionary<PooledObjectName, List<GameObject>>();
        pools.Add(PooledObjectName.Bullet,
            new List<GameObject>(GameConstants.InitialBulletPoolCapacity));
        pools.Add(PooledObjectName.Enemy,
            new List<GameObject>(GameConstants.InitialEnemyPoolCapacity));

        // fill bullet pool
        for (int i = 0; i < pools[PooledObjectName.Bullet].Capacity; i++)
        {
            pools[PooledObjectName.Bullet].Add(GetNewObject(PooledObjectName.Bullet));
        }

        // fill enemy pool
        for (int i = 0; i < pools[PooledObjectName.Enemy].Capacity; i++)
        {
            pools[PooledObjectName.Enemy].Add(GetNewObject(PooledObjectName.Enemy));
        }
    }
	
    public static GameObject GetBullet()
    {
        return GetPooledObject(PooledObjectName.Bullet);
    }

    public static GameObject GetEnemy()
    {
        return GetPooledObject(PooledObjectName.Enemy);
    }

    static GameObject GetPooledObject(PooledObjectName name)
    {
        List<GameObject> pool = pools[name];

        // check for available object in pool
        if (pool.Count > 0)
        {
			// remove object from pool and return
            GameObject obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            return obj;
        }
        else
        {
            // pool empty, so expand pool and return new object
            pool.Capacity++;
            if (name == PooledObjectName.Bullet)
            {
                return GetNewObject(PooledObjectName.Bullet);
            }
            else
            {
                return GetNewObject(PooledObjectName.Enemy);
            }
        }
    }

    public static void ReturnBullet(GameObject bullet)
    {
        ReturnPooledObject(PooledObjectName.Bullet, bullet);
    }

    public static void ReturnEnemy(GameObject enemy)
    {
        ReturnPooledObject(PooledObjectName.Enemy, enemy);
    }

    public static void ReturnPooledObject(PooledObjectName name,
        GameObject obj)
    {
        obj.SetActive(false);
        if (name == PooledObjectName.Bullet)
        {
            obj.GetComponent<Bullet>().StopMoving();
        }
        else
        {
            obj.GetComponent<Enemy>().Deactivate();
        }
        pools[name].Add(obj);
    }

    static GameObject GetNewObject(PooledObjectName name)
    {
        GameObject obj;
        if (name == PooledObjectName.Bullet)
        {
            obj = GameObject.Instantiate(prefabBullet);
            bulletCreated.Invoke(obj);
            obj.GetComponent<Bullet>().Initialize();
        }
        else
        {
            obj = GameObject.Instantiate(prefabEnemy);
            enemyCreated.Invoke(obj);
            obj.GetComponent<Enemy>().Initialize();
        }
        obj.SetActive(false);
        GameObject.DontDestroyOnLoad(obj);
        return obj;
    }

    public static void EmptyPools()
    {
        foreach (KeyValuePair<PooledObjectName, List<GameObject>> kvp in pools)
        {
            pools[kvp.Key].Clear();
        }
    }
}
