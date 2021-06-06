using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public bool timerIsRunning = false;
    public Text timeText;
    public double time;
    private void Start()
    {
        startTimer();
    }
    public void startTimer()
    {
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            time += Time.deltaTime;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public int sec
    {
        get {
            return (int)Math.Floor(time);
        }
    }
}