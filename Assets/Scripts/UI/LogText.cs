using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This class pulls messages from a ROS Subscriber and displays them on the log menu. Messages come in three levels: info, warning, error.
/// </summary>
public class LogText : Singleton<LogText>
{
    public enum LogLevel {info, warning, error};
    
    // Operator Log variables
    private string operatorText = "";
    private int operatorUnreadCount = 0;

    [Header("Operator GameObject References")]
    public TextMeshProUGUI operatorUnreadCountTextMesh;
    public TextMeshProUGUI operatorLogTextMesh;
    public GameObject operatorToastrPrefab;

    [Header("Misc")]
    public Canvas canvas;
    [Tooltip("How long the toastr is displayed")]
    public float toastrTimer = 2;

    DateTime time;


    /// <summary>
    /// Check if logs have been read and pulls from subscriber
    /// </summary>
    public void Update()
    {
        // check if logs have been read
        if (operatorLogTextMesh.IsActive() == true)
        {
            operatorUnreadCount = 0;
            UpdateOperatorUnreadCounter();
        }

        // Pull from subscriber
        if (LogSubscriber.Instance.MessageQueueCount() != 0)
        {
            AudioManager.Instance.PlayMessageSound();

            RosSharp.RosBridgeClient.Message messageObject = LogSubscriber.Instance.DequeueOperatorMessage();

            AddOperatorLogMessage(messageObject);
        }
        
    }

    /// <summary>
    /// Synchronizes unread messages count with the displayed number on screen.
    /// </summary>
    public void UpdateOperatorUnreadCounter()
    {       
        if (operatorUnreadCount == 0)
            operatorUnreadCountTextMesh.SetText("");

        else if (operatorUnreadCount >= 1 && operatorUnreadCount <= 9)
            operatorUnreadCountTextMesh.SetText("" + operatorUnreadCount);

        else
            operatorUnreadCountTextMesh.SetText("9+");
    }

    /// <summary>
    /// Toastr incoming. Start animation and add to log.
    /// </summary>
    /// <param name="message">incoming toastr message</param>
    public void OperatorToastr(String message)
    {
        addToOperatorText(message, LogLevel.error);

        // don't show toastr, if log menu is open anyway
        if (operatorLogTextMesh.IsActive() == true)
            return;

        // instantiate toastr prefab
        GameObject toastr = Instantiate(operatorToastrPrefab, canvas.transform);
        toastr.GetComponentInChildren<TextMeshProUGUI>().SetText(message);

        // start animation of toastr
        StartCoroutine(OperatorToastrAnimationCoroutine(toastr));
    }

    /// <summary>
    /// This coroutine uses a timer for the toaster till it gets put in pocket, the unread counter gets updated and it ultimately gets destroyed
    /// </summary>
    /// <param name="toastr">toastr GameObject reference</param>
    /// <returns></returns>
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
    

    /// <summary>
    /// Add new message to displayed string using HTML-like format and rich text in text mesh pro
    /// </summary>
    /// <param name="message">string of message</param>
    /// <param name="logLevel">log level of message</param>
    public void addToOperatorText(string message, LogLevel logLevel)
    {
        // HTML Color start
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

        // Build string for time
        operatorText = time.Second + operatorText;
        if (time.Second < 10)
            operatorText = "0" + operatorText;

        operatorText = time.Minute + ":" + operatorText;
        if (time.Minute < 10)
            operatorText = "0" + operatorText;

        operatorText = time.Hour + ":" + operatorText;
        if (time.Hour < 10)
            operatorText = "0" + operatorText;

        // HTML Color stop
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

    /// <summary>
    /// New message was pulled and has to be processed
    /// </summary>
    /// <param name="logMessage">message object in RosSharp</param>
    public void AddOperatorLogMessage(RosSharp.RosBridgeClient.Message logMessage)
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
