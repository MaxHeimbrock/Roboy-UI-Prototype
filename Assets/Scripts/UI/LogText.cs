using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogText : Singleton<LogText>
{
    // Roboy Log variables
    private string roboyText = "Start Log";
    private int roboyUnreadCount = 0;

    [Header("Roboy GameObject References")]
    public TextMeshProUGUI roboyUnreadCountTextMesh;
    public TextMeshProUGUI roboyLogTextMesh;
    public GameObject roboyToastrPrefab;

    // Operator Log variables
    private string operatorText = "Start Log";
    private int operatorUnreadCount = 0;

    [Header("Operator GameObject References")]
    public TextMeshProUGUI operatorUnreadCountTextMesh;
    public TextMeshProUGUI operatorLogTextMesh;
    public GameObject operatorToastrPrefab;

    [Header("Misc")]
    public Canvas canvas;

    DateTime time;

    public void Start()
    {
        //OperatorToastr("testtest123");
    }

    // Check if logs have been read
    public void Update()
    {
        /*
         * Not yet implemented
         * 
        if (roboyLogTextMesh.IsActive() == true)
        {
            unreadRoboyLog = 0;
            UpdateRoboyUnreadCounter();
        }
        */

        if (operatorLogTextMesh.IsActive() == true)
        {
            operatorUnreadCount = 0;
            UpdateOperatorUnreadCounter();
        }
    }

    public void UpdateOperatorUnreadCounter()
    {
        if (operatorUnreadCount == 0)
            operatorUnreadCountTextMesh.SetText("");

        else if (operatorUnreadCount >= 1 && operatorUnreadCount <= 9)
            operatorUnreadCountTextMesh.SetText("" + operatorUnreadCount);

        else
            operatorUnreadCountTextMesh.SetText("9+");
    }

    public void UpdateRoboyUnreadCounter()
    {
        if (roboyUnreadCount == 0)
            roboyUnreadCountTextMesh.SetText("");

        else if (roboyUnreadCount >= 1 && roboyUnreadCount <= 9)
            roboyUnreadCountTextMesh.SetText("" + roboyUnreadCount);

        else
            roboyUnreadCountTextMesh.SetText("9+");
    }

    public void OperatorToastr(String message)
    {
        GameObject toastr = Instantiate(operatorToastrPrefab, canvas.transform);
        toastr.GetComponentInChildren<TextMeshProUGUI>().SetText(message);
    }

    public void RoboyToastr(String message)
    {
        GameObject toastr = Instantiate(roboyToastrPrefab, canvas.transform);
        toastr.GetComponentInChildren<TextMeshProUGUI>().SetText(message);
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
            roboyUnreadCount++;

        UpdateRoboyUnreadCounter();
    }

    public void addToOperatorText(string message)
    {
        // Add new message to existing string with timestamp
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

        // Send new string to text mesh
        operatorLogTextMesh.SetText(operatorText);

        // Update unread counter
        if (operatorLogTextMesh.IsActive() == false)
            operatorUnreadCount++;

        // Display new unread counter
        UpdateOperatorUnreadCounter();
    }
}
