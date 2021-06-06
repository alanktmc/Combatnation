using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public static class StaticClass
{
    public static int CrossSceneInt { get; set; }
}
public class selectedSong : MonoBehaviour
{
    // Start is called before the first frame update
    public songData selectedSongJson;
    public selectingSongs selectingSongsObj;
    public FileInfo selectedSongMP3file;
    public GameObject StartObject;
    public GameObject PlayedSongResultObject;
    public int[] hitCount;
    public string songName;
    public string songDescription;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        hitCount = new int[4];
        if (StaticClass.CrossSceneInt == 1)
        {
            StartObject.SetActive(false);
            PlayedSongResultObject.SetActive(true);
            StaticClass.CrossSceneInt = 0;
        }
    }

    public void setSelectedSong()
    {
        selectedSongMP3file = selectingSongsObj.selectedSongMP3file;
        selectedSongJson = selectingSongsObj.selectedSongJson;
        songName = selectedSongMP3file.Name.Substring(0, selectedSongMP3file.Name.Length - 4);
        Debug.Log("Selected Name:"+ songName);
        songDescription = selectedSongJson.description + "\n" + selectedSongJson.diffculty;
        GameObject[] allRootsOfDontDestroyOnLoad = DontdestroyOnLoadAccessor.Instance.GetAllRootsOfDontDestroyOnLoad();
        selectedSong temp = allRootsOfDontDestroyOnLoad[0].GetComponent<selectedSong>();
        temp.songName = songName;
        //DontDestroyOnLoad(selectingSongsObj);
    }
    public void LoadGameScene()
    {
        string scenename = "PlayerMoveAnim";
        Debug.Log("sceneName to load: " + scenename);
        SceneManager.LoadScene(scenename);
    }
    public void LoadResultScene()
    {
        string scenename = "Menu";
        Debug.Log("sceneName to load: " + scenename);
        StaticClass.CrossSceneInt = 1;
        SceneManager.LoadScene(scenename);

    }
}
