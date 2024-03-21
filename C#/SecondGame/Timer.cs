using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private int timerMinutes;
    private int timerSeconds;
    private int timerSeconds100;

    private float startTime;
    private float stopTime;
    private float timerTime;

    private bool isRunning = false;
    private bool started = false;

    [SerializeField] private int wantedTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (started == true)
        {
            timerTime = stopTime + (Time.time - startTime);
            timerMinutes = (int)timerTime / 60;
            timerSeconds = (int)timerTime % 60;
            timerSeconds100 = (int)(Mathf.Floor((timerTime - (timerSeconds + timerMinutes * 60)) * 100));
            if (timerSeconds >= wantedTime)
            {
                TimerStop();
                TimerReset();
            }
        }
    }

    public float TimeLeft()
    {
        return timerTime;
    }

    public float maxTime()
    {
        return wantedTime;
    }

    public void TimerStart()
    {
        if (isRunning == false)
        {
            isRunning = true;
            startTime = Time.time;
            started = true;
        }
    }
    public void TimerStop()
    {
        if (isRunning == true)
        {
            isRunning = false;
            stopTime = Time.time;
            started = false;
        }
    }
    public void TimerReset()
    {
        timerMinutes = 0;
        timerSeconds = 0;
        timerSeconds100 = 0;
        timerTime = 0;
        startTime = 0;
        stopTime = 0;

    }
    public bool isReady()
    {
        if (isRunning == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
