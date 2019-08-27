using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Roboy;

public class MockOperatorLogSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification>
{
    /// <summary>
    /// Holds the currently received data for other objects to read
    /// </summary>
    private string messageData;

    /// <summary>
    /// Start method of TestSubscriber.
    /// Starts a coroutine to initialize the subscriber after 1 second to prevent race conditions.
    /// </summary>
    protected override void Start()
    {
        StartCoroutine(startSubscriber(1.0f));
    }

    /// <summary>
    /// Initializes the subscriber.
    /// </summary>
    /// <param name="waitTime"> defines the time, after that subscriber is initialized.</param>
    /// <returns></returns>
    private IEnumerator startSubscriber(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            base.Start();
            Debug.Log("Started Operator Log Subscriber");
            break;
        }
    }

    /// <summary>
    /// This handler is called, whenever a message on the subscribed topic is received.
    /// </summary>
    /// <param name="message"> is the received message.</param>
    protected override void ReceiveMessage(ErrorNotification message)
    {
        Debug.Log("Received Mock Operator Toastr");
        LogText.Instance.OperatorToastr(message.msg);
    }
}
