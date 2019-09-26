
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
/// <summary>
/// Pose subscriber.
/// </summary>
public class PoseSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.Pose>
{
    /// <summary>
    /// Holds a queue of messages to be read one after the other from the manager.
    /// </summary>
    private Queue<RosSharp.RosBridgeClient.Messages.Roboy.Pose> posesQueue;
    /// <summary>
    /// Enqueues the pose message queue.
    /// </summary>
    /// <param name="msg">Message.</param>
    public void EnqueuePoseMessage(RosSharp.RosBridgeClient.Messages.Roboy.Pose msg)
    {
        posesQueue.Enqueue(msg);
    }
    /// <summary>
    /// Dequeues the pose message queue.
    /// </summary>
    /// <returns>The pose message.</returns>
    public RosSharp.RosBridgeClient.Messages.Roboy.Pose DequeuPoseMessage()
    {
        return posesQueue.Dequeue();
    }
    /// <summary>
    /// Counts the number of objects in the queue.
    /// </summary>
    /// <returns>The queue count.</returns>
    public int MessageQueueCount()
    {
        return posesQueue.Count;
    }
    /// <summary>
    /// Start the subscriber and the coroutine.
    /// </summary>
    protected override void Start()
    {
        posesQueue = new Queue<RosSharp.RosBridgeClient.Messages.Roboy.Pose>();
        StartCoroutine(startSubscriber(1.0f));
    }
    /// <summary>
    /// Starts the subscriber.
    /// </summary>
    /// <returns>The subscriber.</returns>
    /// <param name="waitTime">Wait time.</param>
    private IEnumerator startSubscriber(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            base.Start();
            break;
        }
    }
    /// <summary>
    /// Receives the pose message by enqueueing the queue.
    /// </summary>
    /// <param name="message">Message.</param>
    protected override void ReceiveMessage(RosSharp.RosBridgeClient.Messages.Roboy.Pose message)
    {
        EnqueuePoseMessage(message);
    }
    /// <summary>
    /// Receive the specified message.
    /// </summary>
    /// <param name="message">Message.</param>
    public void receive(RosSharp.RosBridgeClient.Messages.Roboy.Pose message)
    {
        ReceiveMessage(message);
    }
}
