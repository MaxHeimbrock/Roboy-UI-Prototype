using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogText : Singleton<LogText>
{
    public enum LogLevel {info, warning, error};

    // Roboy Log variables
    private string roboyText = "";
    private int roboyUnreadCount = 0;

    [Header("Roboy GameObject References")]
    public TextMeshProUGUI roboyUnreadCountTextMesh;
    public TextMeshProUGUI roboyLogTextMesh;
    public GameObject roboyToastrPrefab;

    // Operator Log variables
    private string operatorText = "";
    private int operatorUnreadCount = 0;

    [Header("Operator GameObject References")]
    public TextMeshProUGUI operatorUnreadCountTextMesh;
    public TextMeshProUGUI operatorLogTextMesh;
    public GameObject operatorToastrPrefab;

    [Header("Misc")]
    public Canvas canvas;
    public float toastrTimer = 2;

    DateTime time;

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

        //if (Input.GetKeyDown(KeyCode.O))
        //    SendOperatorLogMessage("Omnimill selfdestruct", LogLevel.error);
                
        // Pull from subscriber
        if (SuperSubscriber.Instance.MessageQueueCount() != 0)
        {
            AudioManager.Instance.PlayMessageSound();

            RosSharp.RosBridgeClient.Message messageObject = SuperSubscriber.Instance.DequeueOperatorMessage();

            SendOperatorLogMessage(messageObject);
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
        addToOperatorText(message, LogLevel.error);

        if (operatorLogTextMesh.IsActive() == true)
            return;
        GameObject toastr = Instantiate(operatorToastrPrefab, canvas.transform);
        toastr.GetComponentInChildren<TextMeshProUGUI>().SetText(message);

        StartCoroutine(OperatorToastrAnimationCoroutine(toastr));
    }

    // This coroutine uses a timer for the toaster till it gets put in pocket, the unread counter gets updated and it ultimately gets destroyed
    IEnumerator OperatorToastrAnimationCoroutine(GameObject toastr)
    {
        yield return new WaitForSeconds(toastrTimer);
        toastr.GetComponent<Animator>().SetTrigger("PutInPocket");
        yield return new WaitForSeconds(0.7f);
        
        // Update unread counter
        if (operatorLogTextMesh.IsActive() == false)
            operatorUnreadCount++;

        // Display new unread counter
        UpdateOperatorUnreadCounter();

        yield return new WaitForSeconds(5f);

        Destroy(toastr);
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

        // Update unread counter
        if (roboyLogTextMesh.IsActive() == false)
            roboyUnreadCount++;

        UpdateRoboyUnreadCounter();
    }

    public void addToOperatorText(string message, LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogLevel.warning:
                operatorText = "</color>" + operatorText;
                break;
            case LogLevel.info:
                break;
            case LogLevel.error:
                operatorText = "</color>" + operatorText;
                break;
        }

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

        switch (logLevel)
        {
            case LogLevel.warning:
                operatorText = "<color=\"orange\">" + operatorText;
                break;
            case LogLevel.info:
                break;
            case LogLevel.error:
                operatorText = "<color=\"red\">" + operatorText;
                break;
        }

        // Send new string to text mesh
        operatorLogTextMesh.SetText(operatorText);
    }

    public void SendOperatorLogMessage(RosSharp.RosBridgeClient.Message logMessage)
    {
        LogLevel logLevel = LogLevel.info;
        string message = "";

        if (logMessage is RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification)
        {
            logLevel = LogLevel.error;
            message = ((RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification)logMessage).msg;
        }
        else if (logMessage is RosSharp.RosBridgeClient.Messages.Roboy.WarningNotification)
        {
            logLevel = LogLevel.warning;
            message = ((RosSharp.RosBridgeClient.Messages.Roboy.WarningNotification)logMessage).msg;
        }
        else if (logMessage is RosSharp.RosBridgeClient.Messages.Roboy.InfoNotification)
        {
            logLevel = LogLevel.info;
            message = ((RosSharp.RosBridgeClient.Messages.Roboy.InfoNotification)logMessage).msg;
        }

        if (logLevel == LogLevel.error)
        {
            //Debug.Log("Toastr");
            OperatorToastr(message);
        }

        else if (logLevel == LogLevel.info || logLevel == LogLevel.warning)
        {
            //Debug.Log("Info/Warning");
            addToOperatorText(message, logLevel);

            // Update unread counter
            if (operatorLogTextMesh.IsActive() == false)
                operatorUnreadCount++;

            // Display new unread counter
            UpdateOperatorUnreadCounter();
        }
    }
}
