using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogText : Singleton<LogText>
{
    private string roboyText = "Start Log";
    private string operatorText = "Start Log";

    private int unreadRoboyLog = 0;
    public int unreadOperatorLog = 0;

    public TextMeshProUGUI roboyLogTextMesh;
    public TextMeshProUGUI operatorLogTextMesh;

    public GameObject toastrPrefab;
    public Canvas canvas;

    DateTime time;

    public void Start()
    {
        Instantiate(toastrPrefab, canvas.transform);
    }

    // Check if logs have been read
    public void Update()
    {
        /*
         * Not yet implemented
         * 
        if (roboyLogTextMesh.IsActive() == true)
            unreadRoboyLog = 0;
        */

        if (operatorLogTextMesh.IsActive() == true)
            unreadOperatorLog = 0;
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

        roboyLogTextMesh.SetText(roboyText);

        if (roboyLogTextMesh.IsActive() == false)
            unreadRoboyLog++;
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

        operatorLogTextMesh.SetText(operatorText);

        if (operatorLogTextMesh.IsActive() == false)
            unreadOperatorLog++;
    }
}
