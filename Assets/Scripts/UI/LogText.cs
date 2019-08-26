using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogText : Singleton<LogText>
{
    private string roboyText = "Start Log";
    private string operatorText = "Start Log";
    DateTime time;

    public TextMeshProUGUI roboyLogTextMesh;
    public TextMeshProUGUI operatorLogTextMesh;

    public void Start()
    {
        
    }

    public void addToRoboyText(string message)
    {
        time = DateTime.Now;

        roboyText = " - " + message + "\n" + roboyText;

        roboyText = time.Second + roboyText;
        if (time.Second < 10)
            roboyText = "0" + roboyText;

        roboyText = time.Minute + ":" + roboyText;
        if (time.Minute < 10)
            roboyText = "0" + roboyText;

        roboyText = time.Hour + ":" + roboyText;
        if (time.Hour < 10)
            roboyText = "0" + roboyText;

        // Hier ist noch ein Bug
        roboyLogTextMesh.SetText(roboyText);
    }

    public void addToOperatorText(string message)
    {
        time = DateTime.Now;

        operatorText = " - " + message + "\n" + operatorText;

        operatorText = time.Second + operatorText;
        if (time.Second < 10)
            operatorText = "0" + operatorText;

        operatorText = time.Minute + ":" + operatorText;
        if (time.Minute < 10)
            operatorText = "0" + operatorText;

        operatorText = time.Hour + ":" + operatorText;
        if (time.Hour < 10)
            operatorText = "0" + operatorText;

        // Hier ist noch ein Bug
        operatorLogTextMesh.SetText(operatorText);
    }
}
