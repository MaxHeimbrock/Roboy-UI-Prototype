using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

using System;

public class ClickerWink : Clicker
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
    
    int width;
    int height;

    bool OpenCV_ready = false;

    int code = 0;
    int x = -1;
    int y = -1;

    protected override void SubclassStart()
    {                
        int result = OpenCV_Dll.Init(ref width, ref height);

        if (result == -2)
            Debug.Log("Failed to open camera stream.");
        else
        {
            OpenCV_ready = true;
            Debug.Log("width: " + width + " - height: " + height);
        }
    }

    public void Update()
    {
        Debug.Log("Before: code = " + code + " - x = " + x + " - y = " + y);
        
        OpenCV_Dll.Operate(ref code, ref x, ref y);

        Debug.Log("After: code = " + code + " - x = " + x + " - y = " + y);

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
