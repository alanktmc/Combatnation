using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environment : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject envir;
    public float angle,speed;
    Quaternion temp;
    public BoxerMovements enemystatus;
    public AudioSource bgm;
    public AudioSource bell;
    void Start()
    {
         temp = transform.rotation;
        //bgm.Play();
        bgm.Play();

        //bgm.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!enemystatus.enemy_animation_controller.GetCurrentAnimatorStateInfo(0).IsName("win"))
        {
            transform.rotation = Quaternion.Euler(temp.x, angle * speed, temp.z);
            angle++;
        }

        if (bgm.volume<=0.15)
        bgm.volume = bgm.volume + 0.01f * Time.deltaTime;

        if (bgm.volume <= 0.09 && bgm.volume >= 0.08)
            bell.Play();
    }
}
