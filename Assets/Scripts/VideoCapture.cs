using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;
using System;

public class VideoCapture : MonoBehaviour
{
    internal static class OpenCV_Dll
    {
        [DllImport("OpenCV-Roboy-Prototype")]
        internal unsafe static extern int Operate(ref int code, ref int x, ref int y);

        [DllImport("OpenCV-Roboy-Prototype")]
        internal unsafe static extern int Init(ref int outCameraWidth, ref int outCameraHeight);

        [DllImport("OpenCV-Roboy-Prototype")]
        internal unsafe static extern int Close();
    }

    List<IVideoSubscriber> subscribers;

    int width;
    int height;

    bool OpenCV_ready = false;

    int code = 0;
    int x = -1;
    int y = -1;

    void Start()
    {
        int result = OpenCV_Dll.Init(ref width, ref height);

        if (result == -1)
            Debug.Log("Failed to open camera stream.");
        else if (result == -2)
        {
            Debug.Log("Could not find CascadeClassifier.");
        }
        else
        {
            OpenCV_ready = true;
            Debug.Log("width: " + width + " - height: " + height);
        }

        subscribers = new List<IVideoSubscriber>();
    }

    void Update()
    {
        //Debug.Log("Before: code = " + code + " - x = " + x + " - y = " + y);

        OpenCV_Dll.Operate(ref code, ref x, ref y);

        //Debug.Log("After: code = " + code + " - x = " + x + " - y = " + y);
        
        SendPushNotification(code);
        
    }

    public void Subscribe(IVideoSubscriber subscriber)
    {
        subscribers.Add(subscriber);
    }

    private void SendPushNotification(int code)
    {
        foreach (IVideoSubscriber subscriber in subscribers)
        {
            subscriber.ReceivePushNotification(code);
        }
    }

    private void OnDestroy()
    {
        if (OpenCV_ready == true)
        {
            OpenCV_Dll.Close();
        }
    }

    void OnApplicationQuit()
    {
        if (OpenCV_ready == true)
        {
            OpenCV_Dll.Close();
        }
    }
}
