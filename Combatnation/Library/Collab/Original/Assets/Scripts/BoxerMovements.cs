using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerMovements : MonoBehaviour
{
    // Start is called before the first frame update
    //goloves
    public GameObject gloves;
    Animator glovecontroller;
    //enemy
    public GameObject enemy;
    public Animator enemy_animation_controller;
    //arduino
    public SerialController serialController;
    //temporiaily keyboard control
    public string message;

    public bool heavyeffect = false; //for heavy effect
    public bool lighteffect = false; //for light effect
    //##########
    // updated

    //###########
    //
    public bool is_heavy = false;
    //left
    bool is_attacka_clicked = false;   //heavy punch
    bool is_attackaa_clicked = false;  //light punch
    
    //Right
    bool is_attackb_clicked = false;
    bool is_attackbb_clicked = false;
    
    //middle
    bool is_attackc_clicked = false;
    bool is_attackcc_clicked = false;

    //topLeft
    bool is_attackd_clicked = false;
    bool is_attackdd_clicked = false;

    //topRight
    bool is_attacke_clicked = false;
    bool is_attackee_clicked = false;
    //###########
    


    //game manage
    public int punchcount = 0;
    public bool isdefeated = false;
    public GameObject gamemanager;
    public AudioSource punch_sound;
    //Vector3 temppos;
    private pressureScript pressureUI;
    void Awake()
    {
        pressureUI = FindObjectsOfType<pressureScript>()[0];

    }
        void Start()
    {
        enemy_animation_controller = enemy.GetComponent<Animator>();
        glovecontroller = gloves.GetComponent<Animator>();
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        enemy_animation_controller.Play("win");
    }

    // Update is called once per frame
    void Update()
    {
        //bool is_gamestart = gamemanager.GetComponent<Gamemanager>().is_gamestart;
        message = serialController.ReadSerialMessage();

        if (message != null)
        {
            // Check if the message is plain data or a connect/disconnect event.
            if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                Debug.Log("Connection established");
            else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
            else
                Debug.Log("Message arrived: " + message);

        }

        if (Input.GetKeyDown("z"))
        {
            enemy_animation_controller.Play("eattack");
            Debug.Log("PRESSED!!");
        }


        if (Input.GetKeyDown("a") || message == "H")
        {
            is_heavy = true;
            Debug.LogWarning("In heavy mode:"+ is_heavy);
        }


        // if (is_gamestart)
        {
            

            if (Input.GetKeyDown("j") || message == "B")//LEFT
            {
                if (!is_heavy)
                {
                    is_attacka_clicked = true;
                }
                else
                {
                    is_attackaa_clicked = true; 
                }
                
                punch_sound.Play();
                // Debug.Log("a is detected");
            }

            if (Input.GetKeyDown("l") || message == "D")
            {//RIGht
                if (!is_heavy)
                {
                    is_attackb_clicked = true;
                }
                else
                {
                    is_attackbb_clicked = true;
                }
                punch_sound.Play();
            }

            if (Input.GetKeyDown("k") || message == "A")
            {//body
                punch_sound.Play();
                is_attackc_clicked = true;
                if (!is_heavy)
                {
                    is_attackc_clicked = true;
                }
                else
                {
                    is_attackcc_clicked = true;
                }
            }

            if (Input.GetKeyDown("i") || message == "C")
            {
                if (!is_heavy)
                {
                    is_attackd_clicked = true;
                }
                else
                {
                    is_attackdd_clicked = true;
                }
                punch_sound.Play();
            }
            if (Input.GetKeyDown("o") || message == "E")
            {
                if (!is_heavy)
                {
                    is_attacke_clicked = true;
                }
                else
                {
                    is_attackee_clicked = true;
                }
                punch_sound.Play();
            }

            if (is_attacka_clicked )
            {
                enemy_animation_controller.Play("left");
                glovecontroller.Play("l_attack1");
                punchcount++;
                pressureUI.setPressure(0, 0.5f);
                is_attacka_clicked = false;
                lighteffect = true;
            }
            if (is_attackaa_clicked )
            {
                enemy_animation_controller.Play("LeftHeavyHit");
                glovecontroller.Play("l_attack1");
                punchcount++;
                pressureUI.setPressure(0, 1f);
                is_attackaa_clicked = false;
                heavyeffect = true;
                Debug.LogWarning("In heavy mode");
            }


            if (is_attackb_clicked )
            {
                enemy_animation_controller.Play("right");
                glovecontroller.Play("r_attack1");
                punchcount++;
                pressureUI.setPressure(1, 0.5f);
                is_attackb_clicked = false;
                lighteffect = true;

            }
            if (is_attackbb_clicked )
            {
                enemy_animation_controller.Play("RightHeavyHit");
                glovecontroller.Play("r_attack1");
                punchcount++;
                pressureUI.setPressure(1, 1f);
                is_attackbb_clicked = false;
                heavyeffect = true;

            }

            //middle
            if (is_attackc_clicked)
            {
                enemy_animation_controller.Play("front");
                glovecontroller.Play("h_attack");
                punchcount++;
                pressureUI.setPressure(2, 0.5f);
                is_attackc_clicked = false;
                lighteffect = true;
            }
            if (is_attackcc_clicked)
            {
                enemy_animation_controller.Play("MiddleHeavyHit");
                glovecontroller.Play("h_attack");
                punchcount++;
                pressureUI.setPressure(2, 1f);
                is_attackcc_clicked = false;
                heavyeffect = true;

            }

            //top left
            if (is_attackd_clicked )
            {
                if (Random.value > 0.5)
                    enemy_animation_controller.Play("block");
                else
                    enemy_animation_controller.Play("Punch_M3_Left_Dodge");
                glovecontroller.Play("l_attack_top");
                is_attackd_clicked = false;
                pressureUI.setPressure(3, 0.5f);
                punchcount++;
                lighteffect = true;

            }
            if (is_attackdd_clicked)
            {
                enemy_animation_controller.Play("block");
                glovecontroller.Play("l_attack_top");
                pressureUI.setPressure(3, 1f);
                is_attackdd_clicked = false;
                punchcount++;
                heavyeffect = true;

            }

            //top right
            if (is_attacke_clicked)
            {
                if (Random.value > 0.5)
                    enemy_animation_controller.Play("block");
                else
                    enemy_animation_controller.Play("Punch_M3_Right_Dodge");
                glovecontroller.Play("r_attack_top");
                is_attacke_clicked = false;
                pressureUI.setPressure(4, 0.5f);
                punchcount++;
                lighteffect = true;

            }
            if (is_attackee_clicked)
            {
                enemy_animation_controller.Play("block");
                glovecontroller.Play("r_attack_top");
                pressureUI.setPressure(4, 1f);
                is_attackee_clicked = false;
                punchcount++;
                heavyeffect = true;
            }

            if (punchcount >= 40)
            {
                enemy_animation_controller.Play("lose");
                isdefeated = true;
            }
        }







        Debug.Log(message);
        if (message == null)
        {
            return;
        }
    }
}
