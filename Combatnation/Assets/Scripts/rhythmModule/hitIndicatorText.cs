using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;
public class hitIndicatorText : MonoBehaviour
{
    // Start is called before the first frame update
    public Text[] texts;
    private List<bool> startCounting;
    private List<double> timeRemainArray;
    private double prevUpdateTime;
    private timer globalTimer;
    private playHitList playHitListObject;
    void Awake()
    {
        globalTimer = FindObjectsOfType<timer>()[0];
        playHitListObject = FindObjectsOfType<playHitList>()[0];
        startCounting = new List<bool>();
        timeRemainArray = new List<double>();
        for (int i = 0; i < texts.Length; i++)
        {
            startCounting.Add(false);
            timeRemainArray.Add(0.0f);
            texts[i].text = "";
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playHitListObject = FindObjectsOfType<playHitList>()[0];
        if (playHitListObject.startedPlay)
        {
            //Debug.LogWarning("started");
            timeRemainArray = playHitListObject.getNextRemain();
            for (int i = 0; i < texts.Length; i++)
            {
                if (timeRemainArray[i]>=0)
                {


                    //timeRemainArray[i] -= (globalTimer.time - prevUpdateTime);
                    int temp1 = (int)Math.Floor(timeRemainArray[i]);
                    if (temp1 != -1)
                    {
                        texts[i].text = temp1.ToString();
                        //Debug.LogWarning(" "+i+" "+texts[i].text);
                        setColor(i, temp1);
                    }

                }
                else
                {
                    //startCounting[i] = false;
                    texts[i].text = "";
                }
            }
            prevUpdateTime = globalTimer.time;
        }
        
    }
    public void setCountDown(int index,double time)
    {
        if (!startCounting[index])
        {
            startCounting[index] = true;
            timeRemainArray[index] = time;
        }

    }
    private void setColor(int index ,int sec)
    {
        if (sec >= 3)
        {
            texts[index].color = new Color(0.5f, 0f, 1f);
        }
        else if(sec >= 2)
        {
            texts[index].color = new Color(1f, 0f, 1f);
        }
        else
        {
            texts[index].color = new Color(1f, 0f, 0f);
        }
        
    }
}
