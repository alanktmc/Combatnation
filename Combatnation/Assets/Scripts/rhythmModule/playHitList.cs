using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class playHitList : MonoBehaviour
{
    // Start is called before the first frame update
    public hitIndicatorText textHandler;
    public string songName;
    static public List<List<double>> waitingList;
    static public List<List<musicNote>> waitingMusicNoteList;
    private double prevTimeSec;
    public AudioSource musicSouce;
    public static double musicDuration;
    private double startSongTimeSec;
    private timer globalTimer;
    private songData song;
    private int songPointer;
    public double preLoadBufferSec = 3;
    private double pastBufferSec = 1;
    static public double goodHitTimeMinSec = -0.8;
    static public double goodHitTimeMaxSec = 0.8;
    static public double perfectHitTimeMinSec = -0.3;
    static public double perfectHitTimeMaxSec = 0.3;
    public hitEffect[] hitEffects;
    public int[] hitCount ;//
    public bool startedPlay;
    public selectedSong selectedtopic;
    public GameObject ComboObject;
    private bool inCombo;
    private int ComboCount;
    System.Random rnd;
    private void Awake()
    {
        
        rnd = new System.Random();
        textHandler = FindObjectsOfType<hitIndicatorText>()[0];
        globalTimer = FindObjectsOfType<timer>()[0];
        hitCount = new int[4];
    }
    void Start()
    {
        inCombo = false;
        ComboCount = 0;
        setCombo(false);
        GameObject[] allRootsOfDontDestroyOnLoad = DontdestroyOnLoadAccessor.Instance.GetAllRootsOfDontDestroyOnLoad();
        selectedtopic = allRootsOfDontDestroyOnLoad[0].GetComponent<selectedSong>();
        if (selectedtopic != null)
        {
            songName = selectedtopic.songName;
        }
        else
        {
            songName = "Xomu & Justin Klyvis - Setsuna (Kirara Magic Remix)_1min";
        }
        song = readJson.LoadJsonFromFile(songName);
        init();
        StartCoroutine(waitSecondAndStart(5));
    }

    private void init()
    {
        startedPlay = false;
        waitingList = new List<List<double>>();//buffer remaining time of each hit 
        waitingMusicNoteList = new List<List<musicNote>>();
        for (int i = 0; i < textHandler.texts.Length; ++i)
        {
            waitingList.Add(new List<double>());
            waitingMusicNoteList.Add(new List<musicNote>());
        }
        prevTimeSec = globalTimer.sec;
    }
    public void setCombo(bool trueCombo)
    {
        inCombo = trueCombo;
        if (trueCombo)
        {
            ComboCount += 1;
        }
        else
        {
            ComboCount = 0;
        }
        if (ComboCount >= 10)
        {
            ComboObject.SetActive(true);
            ComboObject.GetComponentInChildren<Text>().text = "Combo:" + ComboCount;
        }
        else
        {
            ComboObject.SetActive(false);
            
        }
    }
    public int attacked(int position)//perfect = 2,good = 1
    {
        if (waitingList[position].Count > 0)
        {
            if(waitingList[position][0] > perfectHitTimeMinSec &&
               waitingList[position][0] < perfectHitTimeMaxSec)
            {
                
                waitingList[position].RemoveAt(0);
                waitingMusicNoteList[position].RemoveAt(0);
                hitEffects[position].play(2+1);
                hitCount[2 + 1] += 1;
                setCombo(true);
                return 2;
            }
            else if (waitingList[position][0] > goodHitTimeMinSec &&
               waitingList[position][0] < goodHitTimeMaxSec)
            {

                waitingList[position].RemoveAt(0);
                waitingMusicNoteList[position].RemoveAt(0);
                hitEffects[position].play(1 + 1);
                hitCount[1 + 1] += 1;
                setCombo(true);
                return 1;
            }
            waitingList[position].RemoveAt(0);
            waitingMusicNoteList[position].RemoveAt(0);
            hitEffects[position].play(-1 + 1);
            hitCount[-1 + 1] += 1;
            setCombo(false);
            return -1;// mark as missed.


        }
        else
        {
            hitEffects[position].play((-1 + 1));
            hitCount[-1 + 1] += 1;
            setCombo(false);
            return -1;
        }

    }
    public List<double> getNextRemain()
    {
        prevTimeSec = globalTimer.time;
        List<double> result = new List<double>();
        for (int i = 0; i < waitingList.Count; ++i)
        {
            if (waitingList[i].Count > 0)
                result.Add(waitingList[i][0]);
            else
            {
                result.Add(-1);
            }
        }
        return result;
    }
    private void updateBuffer()
    {
        if (startedPlay)
        {
            double recTemp = globalTimer.time;
            while (songPointer < song.noteList.Length)
            {
                if (song.noteList[songPointer].second + startSongTimeSec < recTemp + preLoadBufferSec)
                {
                    double temp = song.noteList[songPointer].second;
                    int pos = song.noteList[songPointer].positionNumber;
                    waitingList[pos].Add(preLoadBufferSec);
                    waitingMusicNoteList[pos].Add(song.noteList[songPointer]);
                    songPointer++;
                }
                else
                {
                    break;
                }
            }
            for (int j = 0; j < waitingList.Count; ++j)
            {
                if (waitingList[j].Count>0)
                while (waitingList[j][0] < 0)
                {
                    waitingList[j].RemoveAt(0);
                    waitingMusicNoteList[j].RemoveAt(0);
                    hitEffects[j].play(-1 + 1);
                    hitCount[-1 + 1] += 1;
                    setCombo(false);
                        if (waitingList[j].Count == 0)
                    {
                        break;
                    }
                }
            }
            for (int j = 0; j < waitingList.Count; ++j)
            {
                for (int i = 0; i < waitingList[j].Count; ++i)
                {
                    waitingList[j][i] -= (recTemp - prevTimeSec);
                }
            }
            prevTimeSec = recTemp;
        }
        



    }
    public void startSongHit()
    {

        startSongTimeSec = globalTimer.time;
        prevTimeSec = globalTimer.time;
        songPointer = 0;
        startedPlay = true;
        musicSouce.Play();
        StartCoroutine(RunEndAfterMusicEnd(musicSouce));
    }
    IEnumerator waitSecondAndStart(int s)
    {
        yield return new WaitForSeconds(s);
        caloriesCalc tempObj = FindObjectsOfType<caloriesCalc>()[0];
        tempObj.isBoxing = true;
        startSongHit();
        yield return null;
    }
    IEnumerator RunEndAfterMusicEnd(AudioSource audio)
    {
        
        yield return new WaitForSeconds((float)musicDuration);
        musicSouce.Stop();
        endRhythmMusic();
        yield return null;
    }
    public void endRhythmMusic()
    {
        for ( int i = 0; i < hitCount.Length; ++i)
        {
            selectedtopic.hitCount[i] = hitCount[i];
            Debug.Log("selectedtopic.hitCount[i]"+ selectedtopic.hitCount[i]);
        }
        Debug.Log("End Song and Change Scene");
        selectedtopic.LoadResultScene();


    }
    // Update is called once per frame
    void Update()
    {
        updateBuffer();
        if (Input.GetKeyDown(KeyCode.S))
        {
            init();
            startSongHit();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            attacked(4);
        }

    }

}
