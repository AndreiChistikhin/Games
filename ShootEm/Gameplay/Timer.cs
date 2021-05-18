using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    float totalSeconds = 0;

    float elapsedSeconds = 0;
    bool running = false;

    bool started = false;
    TimerFinished timerFinished = new TimerFinished();

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

    public bool Running
    {
        get { return running; }
    }

    public float SecondsLeft
    {
        get 
        {
            if (running)
            {
                return totalSeconds - elapsedSeconds; 
            }
            else
            {
                return 0;
            }
        }
    }

    void Update()
    {   
        // update timer and check for finished
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            if (elapsedSeconds >= totalSeconds)
            {
                running = false;
                timerFinished.Invoke();
            }
        }
    }

    public void Run()
    {   
        // only run with valid duration
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
            elapsedSeconds = 0;
        }
    }

    public void Stop()
    {
        started = false;
        running = false;
    }

    public void AddTime(float seconds)
    {
        totalSeconds += seconds;
    }

    public void AddTimerFinishedListener(UnityAction listener)
    {
        timerFinished.AddListener(listener);
    }
}
