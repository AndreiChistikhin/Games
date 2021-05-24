using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
	float totalSeconds;
	float elapsedSeconds;
	bool running;
	int previousCountdownValue;
	bool started;
    bool isStopped;

    public bool Running => running;
    public bool IsStopped => isStopped;

    public float Duration
    {
		set
        {
			if (!running)
            {
				totalSeconds = value;
			}
		}
	}

	public bool Finished
    {
		get { return started && !running; } 
	}

    void Update()
    {
		if (running)
        {
			elapsedSeconds += Time.deltaTime;

			if (elapsedSeconds >= totalSeconds)
            {
				running = false;
				
			}
		}
	}
	
	public void Run()
    {
		if (totalSeconds > 0)
        {
			started = true;
			running = true;
            isStopped = false;
			elapsedSeconds = 0;
		}
	}

	public void Stop()
    {
		started = false;
		running = false;
        isStopped = true;
	}
}
