using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GrannySpawner : MonoBehaviour
{
	
	[SerializeField]
	GameObject prefabGranny;
	UI scriptUI;
	Timer spawnTimer;
	Vector3 spawnLocation;
    float minimumSpawnCoordinate = -445f;
    float maximumSpawnCoordinate = 445f;

    void Start()
    {
		scriptUI = GameObject.FindObjectOfType<UI>().GetComponent<UI>();
		spawnTimer = gameObject.AddComponent<Timer>();
		StartRandomTimer();
	}

    void Update()
    {
		if (spawnTimer.Finished)
		{
			HandleSpawnTimerFinishedEvent();
		}
	}

	private void HandleSpawnTimerFinishedEvent()
	{
		if (GameObject.FindObjectsOfType<Granny>().Length < 7)
        {
			GrannySpawn();
            StartRandomTimer();
        }
	}

    //Spawn granny on an edge of the map
	void GrannySpawn()
    {
        spawnLocation.x = Random.Range(minimumSpawnCoordinate, maximumSpawnCoordinate);
		spawnLocation.y = 0;
		spawnLocation.z = Random.Range(minimumSpawnCoordinate, maximumSpawnCoordinate);
		int i = Random.Range(1, 5);
		if (i == 1)
		{
            spawnLocation.x = minimumSpawnCoordinate;
		}
		else if (i == 2)
		{
            spawnLocation.x = maximumSpawnCoordinate;
        }
		else if (i == 3)
		{
            spawnLocation.z = minimumSpawnCoordinate;
        }
		else 
		{
            spawnLocation.z = maximumSpawnCoordinate;
        }

        Instantiate<GameObject>(prefabGranny, new Vector3(spawnLocation.x, spawnLocation.y, spawnLocation.z), Quaternion.identity);
        scriptUI.GrannyAdded();
	}

	void StartRandomTimer()
    {
		spawnTimer.Duration =Random.Range(25,100);
		spawnTimer.Run();
	}
}
