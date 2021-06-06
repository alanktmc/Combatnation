using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class musicNote
{
    public double second = -1;
    public string position = "";
    public int pressure = 0;
    public int positionNumber = -1;
}
[Serializable]
public class songData

{
    public string songName;
    public string version;
    public string diffculty = "unknown diffculty:default";
    public string description = "unknown description:default";
    public double offset = 0.0;
    // public bool isUseHardWare;
    public musicNote[] noteList;
}
