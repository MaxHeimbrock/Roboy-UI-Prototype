using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerWink : Clicker, IVideoSubscriber
{
    protected override void SubclassStart()
    {
        SubscribeToVideoCapture();
    }

    public void Update()
    {

    }

    public void ReceivePushNotification(int code)
    {
        Debug.Log("Code = " + code + " in ClickerWink");
    }

    public void SubscribeToVideoCapture()
    {
        GameObject.FindGameObjectWithTag("VideoCapture").GetComponent<VideoCapture>().Subscribe(this);
    }
}
