using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerWink : Clicker, IVideoSubscriber
{
    protected override void SubclassStart()
    {
        GameObject.FindGameObjectWithTag("VideoCapture").GetComponent<VideoCapture>().Subscribe(this);
    }

    public void Update()
    {

    }

    public void ReceivePushNotification(int code)
    {
        Debug.Log("Code = " + code);
    }
}
