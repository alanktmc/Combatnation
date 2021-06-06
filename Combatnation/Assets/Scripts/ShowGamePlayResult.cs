using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class ShowGamePlayResult : MonoBehaviour
{
    // Start is called before the first frame update
    public Text songNameDisplay;
    public Text songInfoDisplay;
    public Text ResultDisplay;
    public Text ResultDataDisplay;
    public selectedSong selectedtopic;
    public double standardRatio = 0.8;
    private int[] totalLevelHit;
    private int totalHit;
    public string[] TotalResultName = { "Bad", "Normal", "Good", "Perfect","Bravo" };
    void Start()
    {
        totalLevelHit = new int[4] { 0, 0, 0, 0 };
        totalHit = 0;

        string[] hitResultName = {"Miss   ","Normal","Good","Perfect" };
        GameObject[] allRootsOfDontDestroyOnLoad = DontdestroyOnLoadAccessor.Instance.GetAllRootsOfDontDestroyOnLoad();
        selectedtopic = allRootsOfDontDestroyOnLoad[0].GetComponent<selectedSong>();
        songNameDisplay.text = selectedtopic.songName;
        songInfoDisplay.text = selectedtopic.songDescription;
        for (int i = 0; i < selectedtopic.hitCount.Length; ++i)
        {
            if(i != 1)
            {
                for(int j = 0; j <= i; j++)
                {
                    totalLevelHit[j] += selectedtopic.hitCount[i];
                }
                totalHit += selectedtopic.hitCount[i];
                Debug.Log("hitCount" + i + " " + selectedtopic.hitCount[i]);
                ResultDataDisplay.text += "\n" + hitResultName[i] + "\t\t"+ selectedtopic.hitCount[i];
            }
            
        }
        setResult();
        for (int j = 0; j < totalLevelHit.Length; j++)
        {
            Debug.Log("totalLevelHit" + j + " " + totalLevelHit[j]);
        }

    }
    public void setResult()
    {
        if(totalLevelHit[3]/ (double)totalHit >= 0.999)
        {
            ResultDisplay.text = TotalResultName[4];
        }
        else
        if (totalLevelHit[3] / (double)totalHit >= standardRatio)
        {
            ResultDisplay.text = TotalResultName[3];
        }
        else if (totalLevelHit[2] / (double)totalHit >= standardRatio)
        {
            ResultDisplay.text = TotalResultName[2];
        }
        else if(totalLevelHit[1] / (double)totalHit >= standardRatio)
        {
            ResultDisplay.text = TotalResultName[1];
        }
        else if(totalLevelHit[0] / (double)totalHit >= standardRatio)
        {
            ResultDisplay.text = TotalResultName[0];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
