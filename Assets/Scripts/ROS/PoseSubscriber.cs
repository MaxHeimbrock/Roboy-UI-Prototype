
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

public class PoseSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.Pose>
{
    private Queue<RosSharp.RosBridgeClient.Messages.Roboy.Pose> posesQueue;

    public void EnqueuePoseMessage(RosSharp.RosBridgeClient.Messages.Roboy.Pose msg)
    {
        posesQueue.Enqueue(msg);
    }

    public RosSharp.RosBridgeClient.Messages.Roboy.Pose DequeuPoseMessage()
    {
        return posesQueue.Dequeue();
    }

    public int MessageQueueCount()
    {
        return posesQueue.Count;
    }

    protected override void Start()
    {
        posesQueue = new Queue<RosSharp.RosBridgeClient.Messages.Roboy.Pose>();
        StartCoroutine(startSubscriber(1.0f));
    }

    private IEnumerator startSubscriber(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            base.Start();
            break;
        }
    }

    protected override void ReceiveMessage(RosSharp.RosBridgeClient.Messages.Roboy.Pose message)
    {
        EnqueuePoseMessage(message);
    }

    public void receive(RosSharp.RosBridgeClient.Messages.Roboy.Pose message)
    {
        ReceiveMessage(message);
    }
}
