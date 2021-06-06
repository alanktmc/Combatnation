using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class caloriesCalc : MonoBehaviour
{
    // Start is called before the first frame update
    public float weightKG;

    public float MET;
    public double accTimeInSec;
    private timer sceneTimer;
    private Text display;
    private double prevTime;
    public bool isBoxing = false;
    private BoxerMovements boxer;
    void Awake()
    {
        sceneTimer = FindObjectsOfType<timer>()[0];

        if (!sceneTimer)
        {
            Debug.LogAssertion("Can't find global timer in calories calculation");
        }
        display = gameObject.GetComponent(typeof(Text)) as Text;
        if (!display)
        {
            Debug.LogAssertion("Can't find CaloriesText in calories calculation");
        }
        boxer = FindObjectsOfType<BoxerMovements>()[0];
        if (!display)
        {
            Debug.LogAssertion("Can't find BoxerMovements in calories calculation");

        }
    }
    void Start()
    {
        //isBoxing = true;
        display.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if (isBoxing)
        {
            accTimeInSec += (sceneTimer.time - prevTime);
            //display.text = ((3.5 * MET * weightKG)*(boxer.punchcount*2 + accTimeInSec)/60 / 200).ToString("0.00") + "Kcals";
            display.text = (int)accTimeInSec + " second";
            prevTime = sceneTimer.time;
        }
    }
}
