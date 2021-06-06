using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CircleAnimation : MonoBehaviour {

    public GameObject[] animObjects;
	public Color[] animObjectsColorBackup;
	public Color[] animObjectsColorBackup2;
	// Use this for initialization
	void Awake()
    {
		animObjectsColorBackup = new Color[4];
		for(int i = 0; i <animObjects.Length;i++)
        {
			animObjectsColorBackup[i] = animObjects[i].GetComponentsInChildren<Image>()[0].color;
			//animObjectsColorBackup2[i] = animObjects[i].GetComponentsInChildren<Image>()[2].color;
		}

	}
	public void resetColor()
    {
		for (int i = 0; i < animObjects.Length; i++)
		{
			animObjects[i].GetComponentsInChildren<Image>()[0].color = animObjectsColorBackup[i];
			//animObjects[i].GetComponentsInChildren<Image>()[2].color = animObjectsColorBackup2[i];
		}
	}
	public void setColor(Color newColor)
	{
		for (int i = 0; i < animObjects.Length; i++)
		{
			animObjects[i].GetComponentsInChildren<Image>()[0].color = newColor;
			//animObjects[i].GetComponentsInChildren<Image>()[2].color = newColor;
		}
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject go in animObjects)
        {
            Vector3 angle = go.transform.eulerAngles;

            angle.z += Time.deltaTime * 50f;

            go.transform.eulerAngles = angle;
        }
	}
}
