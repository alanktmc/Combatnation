using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] effectList;
    private List<GameObject> tempList;

    void Awake()
    {
        tempList = new List<GameObject>();
        //Debug.Log(this.gameObject.transform.parent.gameObject.name+ " "+ this.gameObject.transform.parent.gameObject.transform.Find("GoodWord (1)"));
        effectList[0] = this.gameObject.transform.parent.gameObject.transform.Find("MissWord (1)").gameObject;
        effectList[1] = this.gameObject.transform.parent.gameObject.transform.Find("HitWord (1)").gameObject;
        effectList[2] = this.gameObject.transform.parent.gameObject.transform.Find("GoodWord (1)").gameObject;
        effectList[3] = this.gameObject.transform.parent.gameObject.transform.Find("PerfectWord (1)").gameObject;
        foreach (GameObject obj in effectList)
        {
            obj.SetActive(false);
        }
    }
    void Start()
    {
        
    }
    void fixedUpdate()
    {
        
    }
    public void play(int level)
    {
        GameObject newEffect = Instantiate(effectList[level]);
        newEffect.transform.SetParent(this.gameObject.transform.parent, false);
        newEffect.transform.position = effectList[level].transform.position;
        newEffect.SetActive(true);
        tempList.Add(newEffect);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            play(3);
        }
        if (tempList.Count > 0)
        foreach (GameObject obj in tempList)
        {
            if (!obj.GetComponentInChildren<ParticleSystem>().IsAlive())
            {
                obj.SetActive(false);                
                tempList.Remove(obj);
                Destroy(obj);
                break;
            }
        }
    }
}
