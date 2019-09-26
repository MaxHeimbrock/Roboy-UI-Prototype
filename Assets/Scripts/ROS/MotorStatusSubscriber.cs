using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Roboy;
/// <summary>
/// Motor status subscriber.
/// </summary>
public class MotorStatusSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus>
{
    private Queue<RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus> motorStatusQueue;

    protected override void Start()
    {
        motorStatusQueue = new Queue<RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus>();
        StartCoroutine(startSubscriber(1.0f));
    }

    public void EnqueueMotorMessage(RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus msg)
    {
        motorStatusQueue.Enqueue(msg);
    }

    public RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus DequeueMotorMessage()
    {
        return motorStatusQueue.Dequeue();
    }

    public int MessageQueueCount()
    {
        return motorStatusQueue.Count;
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

    protected override void ReceiveMessage(RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus message)
    {
        EnqueueMotorMessage(message);      
    }
}
