using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerWink : Clicker, IVideoSubscriber
{
    float winkTimeout = 1f;
    float lastWink;

    protected override void SubclassStart()
    {
        SubscribeToVideoCapture();
    }

    public void Update()
    {

    }

    public void ReceivePushNotification(int code)
    {
        Debug.Log("Push to WinkClicker");
        /*
        switch (code)
        {
            case 10:
                break;
            case 11:
                UI_Manager.Click(code);
                break;
            case 12:
                UI_Manager.Click(code);
                break;
            case 13:
                UI_Manager.Click(code);
                break;
            default:
                Debug.Log("OpenCV Dll code: " + code);
                break;
        }
        */
        Debug.Log("Number of dark pixels: " + code);

        if (code < 420)
        {
            float time = Time.time;
            if (time - lastWink > winkTimeout)
            {
                lastWink = Time.time;
                PushClick(1);
            }
        }
    }

    public void SubscribeToVideoCapture()
    {
        GameObject.FindGameObjectWithTag("VideoCapture").GetComponent<VideoCapture>().Subscribe(this);
    }
}
