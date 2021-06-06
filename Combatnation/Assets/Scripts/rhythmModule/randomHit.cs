using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class randomHit : MonoBehaviour
{
    // Start is called before the first frame update
    public hitIndicatorText textHandler;
    private timer globalTimer;
    private int prevTimeSec;
    private int nextAddSec;
    System.Random rnd;
    void Awake()
    {
        rnd = new System.Random();
        textHandler = FindObjectsOfType<hitIndicatorText>()[0];
        globalTimer = FindObjectsOfType<timer>()[0];
    }
    void Start()
    {
        nextAddSec = 5;
    }

    // Update is called once per frame
    void Update()
    {
        int temp = globalTimer.sec;
        if ((temp - prevTimeSec) > 4)
        {
            if ((temp - prevTimeSec) == 5 && nextAddSec == 5)
            {
                textHandler.setCountDown(rnd.Next(5), 6);
                nextAddSec += 1;
            }
            else if ((temp - prevTimeSec) == 6 && nextAddSec == 6)
            {
                textHandler.setCountDown(rnd.Next(5), 6);
                nextAddSec += 1;
            }
            else if ((temp - prevTimeSec) == 7 && nextAddSec == 7)
            {
                textHandler.setCountDown(rnd.Next(5), 6);
                nextAddSec += 1;
            }
            else if (nextAddSec == 8)
            {
                nextAddSec = 5;
                prevTimeSec = temp;
            }
                
        }
    }
}
