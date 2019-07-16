using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Standard;

public class TestSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Standard.String>
{
    public string messageData;

    protected override void Start()
    {
        base.Start();
    }

    protected override void ReceiveMessage(String message)
    {
        messageData = message.data;
    }
}
