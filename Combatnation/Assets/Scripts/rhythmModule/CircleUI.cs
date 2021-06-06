using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] CircleObjTemp;
    public List<List<double>> waitingList;
    public List<List<GameObject>> CircleObjList;
    private List<int> CircleObjListActiveNumber;
    public double maxCircleScale = 0.5;
    public double minCircleScale = 0.05;
    private double preLoadBufferSec;
    public GameObject parent;
    private void Awake()
    {
        waitingList = playHitList.waitingList;
        preLoadBufferSec = FindObjectsOfType<playHitList>()[0].preLoadBufferSec;
        foreach(GameObject obj in CircleObjTemp)
        {
            obj.SetActive(false);
        }
    }
    void Start()
    {
        CircleObjListActiveNumber = new List<int>();
        CircleObjList = new List<List<GameObject>>();
        waitingList = playHitList.waitingList;
        
        //Debug.Log("CircleObjList.Count "+ CircleObjList.Count);
        //Debug.Log(" waitingList.Coun " + waitingList.Count);
    }
    public void setCircle()
    {
        waitingList = playHitList.waitingList;
        //Debug.Log("waitingList.Count"+ waitingList.Count);
        while (CircleObjList.Count < waitingList.Count)
        {
            CircleObjList.Add(new List<GameObject>());
            CircleObjListActiveNumber.Add(0);
        }
        while (CircleObjList.Count > waitingList.Count)
        {
            CircleObjList.RemoveAt(0);
        }
        for (int i = 0; i < waitingList.Count; i++)
        {
            while (CircleObjList[i].Count < waitingList[i].Count)
            {
                GameObject temp = Instantiate(CircleObjTemp[i]);
                
                temp.transform.SetParent(parent.transform.parent, false);
                temp.transform.position = CircleObjTemp[i].transform.position;
                
                CircleObjList[i].Add(temp);
            }
            while (CircleObjListActiveNumber[i] < waitingList[i].Count)
            {
                CircleObjList[i][CircleObjListActiveNumber[i]].SetActive(true);
                CircleObjListActiveNumber[i] += 1;
            }
            while (CircleObjListActiveNumber[i] > waitingList[i].Count)
            {
                CircleObjListActiveNumber[i] -= 1;
                CircleObjList[i][CircleObjListActiveNumber[i]].GetComponent<CircleAnimation>().resetColor();
                CircleObjList[i][CircleObjListActiveNumber[i]].SetActive(false);
                

            }
        }
    }
    public void setCircleSizeAndColor()
    {
        for (int i = 0; i < waitingList.Count; ++i)
        {
            for (int j = 0;j < waitingList[i].Count; ++j)
            {
                //double ratio =( waitingList[i][j] / (preLoadBufferSec + 0.5));
                double ratio = (1 - waitingList[i][j] / (preLoadBufferSec + 0.5));
                double scale = (maxCircleScale - minCircleScale) * ratio + minCircleScale;
                //Debug.Log("i:"+i+" j:"+j+ " scale:"+ scale + " waitingList[i][j]"+ waitingList[i][j]);
                CircleObjList[i][j].GetComponent<RectTransform>().localScale = new Vector3((float)scale,(float)scale,1.0f);
                
                if (waitingList[i][j] > 0 &&waitingList[i][j] < 1 && playHitList.waitingMusicNoteList[i][j].pressure >= 1)
                {

                    CircleObjList[i][j].GetComponent<CircleAnimation>().setColor(new Color(0f, 0f, 1f, 1f));
                }
                if(waitingList[i][j] > 0 &&  waitingList[i][j] < 1 && playHitList.waitingMusicNoteList[i][j].pressure < 1)
                {
                    CircleObjList[i][j].GetComponent<CircleAnimation>().setColor(new Color(1f, 0f, 0f, 1f));
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        setCircle();
        setCircleSizeAndColor();
    }
}
