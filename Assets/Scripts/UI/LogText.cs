using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogText : Singleton<LogText>
{
    private string text = "Start Log";
    DateTime time;

    public TextMeshProUGUI textMesh;

    public void Start()
    {
        
    }

    public void addToLogText(string message)
    {
        time = DateTime.Now;

        text = " - " + message + "\n" + text;

        text = time.Second + text;
        if (time.Second < 10)
            text = "0" + text;

        text = time.Minute + ":" + text;
        if (time.Minute < 10)
            text = "0" + text;

        text = time.Hour + ":" + text;
        if (time.Hour < 10)
            text = "0" + text;

        // Hier ist noch ein Bug
        textMesh.SetText(text);
    }
}
