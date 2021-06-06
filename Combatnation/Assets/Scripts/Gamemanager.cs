using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Gamemanager : MonoBehaviour
{
    //public SerialController serialController;
    public bool is_gamestart = false;
    public BoxerMovements enemystatus;
    public GameObject startbutton;
    public GameObject restartbutton;
    public Slider healthbar;
    // Start is called before the first frame update
    void Start()
    {
        //serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        enemystatus = GameObject.Find("Controller").GetComponent<BoxerMovements>();
        //startbutton.SetActive(true);
        restartbutton.SetActive(false);
       
    }

   public void startgame()
    {
        is_gamestart = true;
        startbutton.SetActive(false);
    }

    public void reloadgame()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    // Update is called once per frame
    void Update()
    {
        //string message = serialController.ReadSerialMessage();

//        bool game_end = enemystatus.isdefeated;



       // if (game_end)
        {
            is_gamestart = false;
            restartbutton.SetActive(true);
        }

       // int healthbarvalue = 10 * enemystatus.punchcount;
     //   if (healthbar.value<=healthbarvalue )
        {
            healthbar.value += (healthbar.value +1)* Time.deltaTime;
        }
    }
}
