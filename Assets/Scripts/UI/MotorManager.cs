using UnityEngine;
using System.Collections;
using System;

public class MotorManager : MonoBehaviour
{
    public GameObject Roboy;  
    //Workaround for demo. Fix to find&match the corresponding motor from roboy 
    public GameObject Motor1;
    public GameObject Motor2;
    RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus message;

    void Start()
    {
        Debug.Log("Start Motor Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<MotorStatusSubscriber>().MessageQueueCount() != 0)
        {
            message = gameObject.GetComponent<MotorStatusSubscriber>().DequeueMotorMessage();
            showMotorStatus();
        }
    }

    private void showMotorStatus()
    {
        if(message != null)
        {
            Debug.Log("Info for motor with ID: " + message.id + " received.");
            switch (message.id)
            {
                case 0:
                    Motor1.SetActive(true);
                    String text = message.current[0].ToString();
                    Motor1.gameObject.transform.Find("MotorWindow").GetChild(1).GetComponent<TextMesh>().text = "Current: " + text;
                    break;
                case 1:
                    Motor2.SetActive(true);
                    String text1 = message.current[0].ToString();
                    Motor2.gameObject.transform.Find("MotorWindow").GetChild(1).GetComponent<TextMesh>().text = "Current: " + text1;
                    break;
                default:
                    break;
            }
        }
    }
}
