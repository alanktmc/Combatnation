using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_control : MonoBehaviour
{
    public GameObject []targets;
    public SerialController serialController;
    public string message;
    public GameObject perfect_text;
    public bool get_message = false;

    bool one_on = false;
    bool two_on = false;
    bool three_on = false;
    bool four_on = false;
    bool five_on = false;

    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        perfect_text.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        message = serialController.ReadSerialMessage();


        if (message != null)
        {
            //Debug.Log(message);
            // Check if the message is plain data or a connect/disconnect event.
            if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
                Debug.Log("Connection established");
            else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
                Debug.Log("Connection attempt failed or disconnection detected");
           else
                Debug.Log("Message arrived: " + message);

        }
        //////led on
        /*
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
        */

        /*
        if ((Input.GetKeyDown("z") || targets[0].activeSelf) && !one_on)
        {
            serialController.SendSerialMessage("1");
            Debug.Log("1");
            one_on = true;
        }
        else if ((!targets[0].activeSelf || Input.GetKeyDown("a"))&& one_on)
        {
            //serialController.SendSerialMessage("6");
            Debug.Log("6");
            one_on = false;
        }

        if ((Input.GetKeyDown("x") || targets[1].activeSelf) &&!two_on)
        {
            serialController.SendSerialMessage("2");
            Debug.Log("2");
            two_on = true;
        }
        else if ((!targets[1].activeSelf || Input.GetKeyDown("s")) && two_on)
        {
            //serialController.SendSerialMessage("7");
            Debug.Log("0");
            two_on = false;
        }

        if ((Input.GetKeyDown("c") || targets[2].activeSelf) && !three_on)
        {
            serialController.SendSerialMessage("3");
            Debug.Log("3");
            three_on = true;

        }
        else if ((!targets[2].activeSelf || Input.GetKeyDown("d")) &&three_on)
        {
            //serialController.SendSerialMessage("8");
            Debug.Log("8");
            three_on = false;
        }

        if ((Input.GetKeyDown("v") || targets[3].activeSelf)&& !four_on)
        {
            serialController.SendSerialMessage("4");
            Debug.Log("4");
            four_on = true;
        }
        else if ((!targets[3].activeSelf || Input.GetKeyDown("f")) && four_on)
        {
            //serialController.SendSerialMessage("9");
            Debug.Log("9");
            four_on = false;
        }

        if ((Input.GetKeyDown("b") || targets[4].activeSelf) && !five_on)
        {
            serialController.SendSerialMessage("5");
            Debug.Log("5");
            five_on = true;
        }
        else if ((!targets[4].activeSelf || Input.GetKeyDown("g")) && five_on)
        {
            //serialController.SendSerialMessage("0");
            Debug.Log("0");
            five_on = false;
        }*/

        
        if (Input.GetKeyDown("w") )
            serialController.SendSerialMessage("1");
        if (Input.GetKeyDown("e") )
            serialController.SendSerialMessage("2");
        if (Input.GetKeyDown("a"))
            serialController.SendSerialMessage("3");
        if (Input.GetKeyDown("s"))
            serialController.SendSerialMessage("4");
        if (Input.GetKeyDown("d"))
            serialController.SendSerialMessage("5");
        
        if (Input.GetKeyDown("i"))
            serialController.SendSerialMessage("6");
        if (Input.GetKeyDown("o"))
            serialController.SendSerialMessage("7");
        if (Input.GetKeyDown("j"))
            serialController.SendSerialMessage("8");
        if (Input.GetKeyDown("k"))
            serialController.SendSerialMessage("9");
        if (Input.GetKeyDown("l"))
            serialController.SendSerialMessage("0");
        
       // if(targets[0].activeSelf && targets[0].GetComponent<AudioSyncScale>().m_isBeat)
         //   serialController.SendSerialMessage("1");




        if (Input.GetKeyDown("i") || message == "A") //
            {
                if (targets[0].activeSelf)
                {
                    targets[0].SetActive(false);
                    perfect_text.SetActive(true);
                    StartCoroutine("kill_perfect");
                    serialController.SendSerialMessage("6");
                }

            }
            if (Input.GetKeyDown("o") || message == "B") //
            {
                if (targets[1].activeSelf)
                {
                    targets[1].SetActive(false);
                    perfect_text.SetActive(true);
                    StartCoroutine("kill_perfect");
                    serialController.SendSerialMessage("7");
                }
            }
            if (Input.GetKeyDown("j") || message == "C") //
            {
                if (targets[2].activeSelf)
                {
                    targets[2].SetActive(false);
                    perfect_text.SetActive(true);
                    StartCoroutine("kill_perfect");
                    serialController.SendSerialMessage("8");
                }
            }
            if (Input.GetKeyDown("k") || message == "D") //
            {
                if (targets[3].activeSelf)
                {
                    targets[3].SetActive(false);
                    perfect_text.SetActive(true);
                    StartCoroutine("kill_perfect");
                    serialController.SendSerialMessage("9");

                }
            }
            if (Input.GetKeyDown("l") || message == "E") //
            {
                if (targets[4].activeSelf)
                {
                    targets[4].SetActive(false);
                    perfect_text.SetActive(true);
                    StartCoroutine("kill_perfect");
                    serialController.SendSerialMessage("0");
                }
            }
        
            //Debug.Log(message);
            if (message == null)
            {
                return;
            }
        }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);

    }
    IEnumerator kill_perfect()
    {
        yield return new WaitForSeconds(0.5f);
        perfect_text.SetActive(false);
    }

}
