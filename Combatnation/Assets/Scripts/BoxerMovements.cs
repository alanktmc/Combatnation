using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerMovements : MonoBehaviour
{
    // Start is called before the first frame update
    //goloves
    public SerialController serialController;
    //temporiaily keyboard control
    public string message;

    //###########



    //game manage
    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {
        message = serialController.ReadSerialMessage();
        Debug.Log(message);
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
        //////led on
        if (Input.GetKeyDown("z"))
            serialController.SendSerialMessage("1");
        if (Input.GetKeyDown("x"))
            serialController.SendSerialMessage("2");
        if (Input.GetKeyDown("c"))
            serialController.SendSerialMessage("3");
        if (Input.GetKeyDown("v"))
            serialController.SendSerialMessage("4");
        if (Input.GetKeyDown("b"))
            serialController.SendSerialMessage("5");

        //////led off (when miss)
        if (Input.GetKeyDown("a"))
            serialController.SendSerialMessage("6");
        if (Input.GetKeyDown("s"))
            serialController.SendSerialMessage("7");
        if (Input.GetKeyDown("d"))
            serialController.SendSerialMessage("8");
        if (Input.GetKeyDown("f"))
            serialController.SendSerialMessage("9");
        if (Input.GetKeyDown("g"))
            serialController.SendSerialMessage("0");


        // if (is_gamestart)
        {


            if (Input.GetKeyDown("j") || message == "B")//LEFT
            {
                // Debug.Log("a is detected");
            }

            if (Input.GetKeyDown("l") || message == "D")
            {//RIGht
            }

            if (Input.GetKeyDown("k") || message == "A")
            {//body

            }

            if (Input.GetKeyDown("i") || message == "C")
            {
                
            }
            if (Input.GetKeyDown("o") || message == "E")
            {
            }



            Debug.Log(message);
            if (message == null)
            {
                return;
            }
        }
    }
}