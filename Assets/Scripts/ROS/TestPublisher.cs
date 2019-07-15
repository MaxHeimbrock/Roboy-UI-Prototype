using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;

public class TestPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Standard.String>
{
    protected override void Start()
    {
        base.Start();
    }

    private void publishMessage(string message)
    {
        RosSharp.RosBridgeClient.Messages.Standard.String rosString = new RosSharp.RosBridgeClient.Messages.Standard.String(message);
        Publish(rosString);
    }

}
