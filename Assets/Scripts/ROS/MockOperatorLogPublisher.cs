﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;

public class MockOperatorLogPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification>
{
    /// <summary>
    /// Start method of TestPublisher.
    /// Starts a coroutine to initialize the publisher after 1 second to prevent race conditions.
    /// </summary>
    protected override void Start()
    {
        StartCoroutine(startPublisher(1.0f));
    }

    /// <summary>
    /// Starts the publisher and sends two test messages for demonstration purposes.
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    private IEnumerator startPublisher(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            base.Start();
            //Debug.Log("Mock Publisher started");
            yield return new WaitForSeconds(waitTime);

            break;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Debug.Log("Send Error Log");
            // Test error message 
            RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification errorMessage = new RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification();
            errorMessage.code = 1;
            errorMessage.@object = "";
            errorMessage.msg = "There is an error";
            errorMessage.extra = "a";
            errorMessage.validity_duration = 1;

            PublishMessage(errorMessage);
        }
    }

    /// <summary>
    /// This method publishes a simple string messages to the topic of the object.
    /// </summary>
    /// <param name="message">is the message, which shall be published.</param>
    private void PublishMessage(RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification message)
    {
        Publish(message);
    }

}
