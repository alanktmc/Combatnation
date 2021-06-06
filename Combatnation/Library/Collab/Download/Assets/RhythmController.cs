using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using NLayer;
using UnityEngine.UI;
using System;
using System.IO;

public class RhythmController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rhythmObj;
    public string songName;
    private AudioClip music;
    private AudioSource musicSouce;
    public Text statusText;
    public int songLoadingStatus; // 0 = not start, 1 = not finished , 2 = finished,-1  = error
    string path;
    private void Awake()
    {
        songLoadingStatus = 0;
        path = Application.dataPath + "/Resources/Songs";
        //Application.persistentDataPath
        Debug.LogWarning("Application.dataPath " + Application.dataPath);
        musicSouce = rhythmObj.GetComponent<AudioSource>();
        songName = "60BPM_1min";
        //musicSouce = rhythmObj.GetComponent<AudioSource>();
        //string path = Path.Combine(Application.dataPath, "Assets", "Resources", "Songs", name);
        
    }
    public int loadSong(string name,bool refresh = false)
    {
        if (songLoadingStatus == 0 || refresh)
        {
            songLoadingStatus = 1;
            string url = string.Format("file:///{0}", path + "/" + name + ".mp3");
            Debug.LogWarning("url " + url);
            StartCoroutine(LoadHelper(url));
        }
        return songLoadingStatus;
    }
    public FileInfo[] readAllSong()
    {
        DirectoryInfo info = new DirectoryInfo(path);
        FileInfo[] fileInfo = info.GetFiles("*.mp3");
        foreach (FileInfo file in fileInfo)
        {
            Debug.LogWarning(file);
        }
        return fileInfo;
    }
    private void Start()
    {
        loadSong(songName);
    }

    IEnumerator LoadHelper(string uri)
    {
        statusText.text = "Loading...";
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            statusText.text = www.error;
            songLoadingStatus = -1;
        }
        else
        {
            statusText.text = "loaded";
            
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
            var memStream = new System.IO.MemoryStream(results);
            var mpgFile = new NLayer.MpegFile(memStream);
            var samples = new float[mpgFile.Length];
            mpgFile.ReadSamples(samples, 0, (int)mpgFile.Length);

            var clip = AudioClip.Create("foo", samples.Length, mpgFile.Channels, mpgFile.SampleRate, false);
            clip.SetData(samples, 0);
            musicSouce.clip = clip;
            songLoadingStatus = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("musicSouce.clip" + musicSouce.clip);
    }
}
