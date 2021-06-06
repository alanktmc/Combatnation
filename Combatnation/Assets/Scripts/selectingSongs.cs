using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;


public class selectingSongs : MonoBehaviour
{
    // Start is called before the first frame update
    string path;
    public GameObject listContentObject;
    public GameObject listTempObject;
    public Text songNameDisplay;
    public Text songInfoDisplay;
    
    public songData selectedSongJson;
    public FileInfo selectedSongMP3file;
    static public FileInfo[] fileInfo;
    static public List<songData> songJsonList;
    public GameObject SongSelectionObject;
    public GameObject PlayedSongResultObject;
    private void Awake()
    {
        

        path = Application.dataPath + "/Resources/Songs";
        songJsonList = new List<songData>();
        readAllSong();


        if (StaticClass.CrossSceneInt == 1)
        {
            SongSelectionObject.SetActive(false);
            PlayedSongResultObject.SetActive(true);
            StaticClass.CrossSceneInt = 0;
        }
        //createElementInList("test");

    }
    void Start()
    {
        
    }
    public void setSelectedSong(songData songJson, FileInfo songInfo)
    {
        selectedSongJson = songJson;
        selectedSongMP3file = songInfo;
    }
    public FileInfo[] readAllSong()
    {
        DirectoryInfo info = new DirectoryInfo(path);
        fileInfo = info.GetFiles("*.mp3");
        songData tempData;
        foreach (FileInfo file in fileInfo)
        {
            Debug.Log(file.Name.Substring(0, file.Name.Length - 4));
            tempData = readJson.LoadJsonFromFile(file.Name.Substring(0, file.Name.Length-4));
            songJsonList.Add(tempData);
            if (tempData != null)
            {
                createElementInList(file.Name.Substring(0, file.Name.Length - 4), tempData, songJsonList.Count-1);
            }
            else
            {
                Debug.Log("Can't read " + file.Name.Substring(0, file.Name.Length - 4)+".json");
            }
            
        }
        return fileInfo;
    }

    private void createElementInList(string text, songData songJson,int index)
    {
        GameObject temp = Instantiate(listTempObject);
        temp.SetActive(true);
        songClickAction songClickActionObj = temp.GetComponent<songClickAction>();
        songClickActionObj.thisSongInfo = songJson.description + "\n" + songJson.diffculty;
        songClickActionObj.thisSongName = songJson.songName;
        songClickActionObj.index = index;
        temp.GetComponentInChildren<Text>().text = songJson.songName;
        temp.transform.SetParent(listContentObject.transform, false);
        temp.transform.position = listTempObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("selectedSongJson:"+ selectedSongJson.songName);
        //Debug.Log("fileInfo:" + selectedSongMP3file.Name);
    }
}
