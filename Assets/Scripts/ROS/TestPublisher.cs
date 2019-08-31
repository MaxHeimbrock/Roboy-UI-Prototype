using System.Collections;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using UnityEngine;

using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Services.RosApi;
using RosSharp.RosBridgeClient.Services.Standard;

public class TestPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Standard.String>
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

            publishMessage("Test-Message-1");
            yield return new WaitForSeconds(waitTime);
            publishMessage("Test-Message-2");

            break;
        }
    }

    /// <summary>
    /// This method publishes a simple string messages to the topic of the object.
    /// </summary>
    /// <param name="message">is the message, which shall be published.</param>
    private void publishMessage(string message)
    {
        RosSharp.RosBridgeClient.Messages.Standard.String rosString = new RosSharp.RosBridgeClient.Messages.Standard.String(message);
        Publish(rosString);
    }

}