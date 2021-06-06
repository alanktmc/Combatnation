using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class songClickAction : MonoBehaviour
{
    public Text songNameDisplay;
    public Text songInfoDisplay;
    public int index;
    public string thisSongName;
    public string thisSongInfo;
    private selectingSongs selectSongObj;
    private void Awake()
    {
        selectSongObj = FindObjectOfType<selectingSongs>();
    }
    public void setName()
    {
        songNameDisplay.text = thisSongName;
        songInfoDisplay.text = thisSongInfo;
        selectSongObj.setSelectedSong(selectingSongs.songJsonList[index], selectingSongs.fileInfo[index]);
    }
}
