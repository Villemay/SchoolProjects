using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int timerMinutes;
    public int timerSeconds;
    public int timerSeconds100;

    public float startTime;
    public float stopTime;
    public float timerTime;

    public bool isRunning = false;
    public bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the timer is started
        if (started == true)
        {
            timerTime = stopTime + (Time.time - startTime);
            timerMinutes = (int)timerTime / 60;
            timerSeconds = (int)timerTime % 60;
            timerSeconds100 = (int)(Mathf.Floor((timerTime - (timerSeconds + timerMinutes * 60)) * 100));
        }
    }
    //starts the timer
    public void TimerStart()
    {
        if (isRunning == false)
        {
            isRunning = true;
            startTime = Time.time;
            started = true;
        }
    }
    //stops the timer
    public void TimerStop()
    {
        if (isRunning == true)
        {
            isRunning = false;
            stopTime = Time.time;
            started = false;
        }
    }
    //resets the timer
    public void TimerReset()
    {
        timerMinutes = 0;
        timerSeconds = 0;
        timerSeconds100 = 0;
        timerTime = 0;
        startTime = 0;
        stopTime = 0;

    }
}
