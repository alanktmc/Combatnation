using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

enum hitPosition
{
    Left,
    Right,
    Middle,
    TopLeft,
    TopRight
}
public class readJson : MonoBehaviour
{
    
    public static songData LoadJsonFromFile(string songName = "Test")
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.dataPath + "/Resources/"+ songName + ".json"))
        {
            return null;
        }
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/" + songName + ".json");
        if (sr == null)
        {
            return null;
        }
        string json = sr.ReadToEnd();
        if (json.Length > 0)
        {
            songData result;
            try
            {
                 result = JsonUtility.FromJson<songData>(json);
            }
            catch 
            {
                Debug.LogWarning("Cannot read " + songName + ".json. Dataformat is wrong");
                return null;
            }
            
            for (int i = 0; i < result.noteList.Length; ++i)
            {
                if (result.noteList[i].second == -1)
                {
                    Debug.LogWarning("Cannot read " + songName+ ".json. Found a -1 sec or empty second node");
                    return null;
                }else
                if(result.noteList[i].position != "" && result.noteList[i].positionNumber == -1)
                {
                    string t = result.noteList[i].position.ToLower();
                    if (t == "left")
                    {
                        result.noteList[i].positionNumber = (int)hitPosition.Left;
                    }else
                    if (t == "right")
                    {
                        result.noteList[i].positionNumber = (int)hitPosition.Right;
                    }
                    else
                    if (t == "middle")
                    {
                        result.noteList[i].positionNumber = (int)hitPosition.Middle;
                    }
                    else
                    if (t == "topleft")
                    {
                        result.noteList[i].positionNumber = (int)hitPosition.TopLeft;
                    }
                    else
                    if (t == "topright")
                    {
                        result.noteList[i].positionNumber = (int)hitPosition.TopRight;
                    }
                    else
                    {
                        Debug.LogWarning("Cannot read " + songName + ".json. Found "+ t + " at position.");
                        return null;
                    }
                    //Debug.LogWarning("result.noteList[i].positionNumber " + result.noteList[i].positionNumber);
                    
                }
                else if(result.noteList[i].positionNumber != -1 && result.noteList[i].position == "")
                {
                    int t = result.noteList[i].positionNumber;
                    if (t == 0)
                    {
                        result.noteList[i].position = hitPosition.Left.ToString();
                    }
                    else
                    if (t == 1)
                    {
                        result.noteList[i].position = hitPosition.Left.ToString();
                    }
                    else
                    if (t == 2)
                    {
                        result.noteList[i].position = hitPosition.Left.ToString();
                    }
                    else
                    if (t == 3)
                    {
                        result.noteList[i].position = hitPosition.Left.ToString();
                    }
                    else
                    if (t == 4)
                    {
                        result.noteList[i].position = hitPosition.Left.ToString();
                    }
                    else
                    {
                        Debug.LogWarning("Cannot read " + songName+ ".json. Find a node do not have correct position Information at " +result.noteList[i].second +" sec.");
                        return null;

                    }
                    //Debug.LogWarning(" result.noteList[i].position" + result.noteList[i].position);
                }
                else
                {
                    Debug.LogWarning("Cannot read " + songName + ".json. Find a node do not have correct position Information at " + result.noteList[i].second + " sec.");
                    return null;
                }
                if(result.noteList[i].second != -1)
                {
                    result.noteList[i].second += result.offset;
                }
            }
            return result;
        }
        return null;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Save();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            songData status = readJson.LoadJsonFromFile();
            Debug.Log("gameName:\t\t" + status.songName);
            Debug.Log("version:\t\t" + status.version);
            Debug.Log("Number statusList:\t" + status.noteList.Length);
            Debug.Log("status.noteList[0].position:\t" + status.noteList[0].position);
            Debug.Log("status.noteList[0].positionNumber:\t" + status.noteList[0].positionNumber);
        }
    }
}