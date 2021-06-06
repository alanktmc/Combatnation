using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialEffect : MonoBehaviour
{
    public GameObject []wordeffect;
    public BoxerMovements boxerMovements;

    public Transform cameratransform;
    Vector3 orginalcamaera;
    public float shakeTime;
    public float shakeamout;
    bool iscamerashake = false;
    float tempshaketime;
    public GameObject speedline;
    // Start is called before the first frame update
    void Start()
    {
        orginalcamaera = cameratransform.localPosition;
        tempshaketime = shakeTime;
        speedline.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (boxerMovements.heavyeffect)
            heavy_effect();

       // if (boxerMovements.lighteffect)
            light_effect();

        if (iscamerashake)
        {
            if (tempshaketime > 0)
            {
                cameratransform.localPosition = orginalcamaera + Random.insideUnitSphere * shakeamout;
                speedline.gameObject.SetActive(true);
                tempshaketime -= Time.deltaTime;
            }
            else
            {
                cameratransform.localPosition = orginalcamaera;
                iscamerashake = false;
                speedline.gameObject.SetActive(false);
                //boxerMovements.is_heavy = false;
                tempshaketime = shakeTime;
            }

        }

     //   if (boxerMovements.isdefeated)
            wordeffect[8].gameObject.SetActive(true);
    }




    void heavy_effect() {

        int i = Random.Range(0, 7);
        wordeffect[i].gameObject.SetActive(true);

        var main = wordeffect[i].GetComponent<ParticleSystem>().main;
        main.startSize = Random.Range(11, 30);
        wordeffect[i].GetComponent<ParticleSystem>().Play();

        iscamerashake = true;
        //Debug.Log("play light punching effect: " + i);
       // boxerMovements.heavyeffect = false;

    }

    void light_effect()
    {
        int i = Random.Range(0, 7);
        wordeffect[i].gameObject.SetActive(true);

        var main = wordeffect[i].GetComponent<ParticleSystem>().main;
        main.startSize = Random.Range(4, 11);
        wordeffect[i].GetComponent<ParticleSystem>().Play();

        //sDebug.Log("play light punching effect: " + i);
        //boxerMovements.lighteffect = false;
    }


}
