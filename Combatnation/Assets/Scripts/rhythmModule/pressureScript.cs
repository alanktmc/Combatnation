using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pressureScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image[] imageChild;
    [Range(0, 1)]
    public float pressure = 0.01f;
    private timer sceneTimer;
    private int[] prevTime;
    void Awake()
    {
        sceneTimer = FindObjectsOfType<timer>()[0];
        prevTime = new int[10];
        if (!sceneTimer)
        {
            Debug.LogAssertion("Can't find global timer in calories calculation");
        }
    }
        //
    void Start()
    {
        //imageChild[4].color = new Color(1, 1, 0f, 1f);
    }
    public int setPressure(int id,float pressure)
    {
        if (pressure > 1 || pressure < 0)
        {
            pressure = Mathf.Min(pressure, 1);
            Debug.LogWarning("The \"pressure\" when using panel.setPressure is incorrect. The \"pressure\" should between 0 and 1 but not " + pressure);
        }
        if (id < 0 || id > imageChild.Length)
        {
            Debug.LogError("The \"id\" when using panel.setPressure is incorrect. The \"id\" should between 0 and "+imageChild.Length+" but not " + id);
            return -1;
        }
        prevTime[id] = sceneTimer.sec;
        imageChild[id].color = new Color(Mathf.Min(pressure * 2, 1), Mathf.Min((1 - pressure) * 2, 1), 0f, 1f);
        return 0;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        //pressure += 0.003f;
        //pressure = Mathf.Min(pressure, 1);
        //setPressure(4,pressure);
        for (int i = 0; i < imageChild.Length; ++i)
        {
            if (prevTime[i] > 0)
            {
                if (sceneTimer.sec - prevTime[i] > 1)
                {
                    imageChild[i].color = new Color(1f, 1f, 1f, 1f);
                    prevTime[i] = -1;
                }
            }
            
        }
        
    }
    void Update()
    {

    }
}
