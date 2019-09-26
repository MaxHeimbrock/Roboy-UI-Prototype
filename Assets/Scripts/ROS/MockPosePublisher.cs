using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;
/// <summary>
/// Mock pose publisher.
/// </summary>
public class MockPosePublisher : Publisher<RosSharp.RosBridgeClient.Messages.Roboy.Pose>
{
    /// <summary>
    /// Start method of MockPosePublisher.
    /// Starts a coroutine to initialize the publisher after 1 second to prevent race conditions.
    /// </summary>
    protected override void Start()
    {
        StartCoroutine(StartPublisher(1.0f));
    }

    /// <summary>
    /// Starts the publisher.
    /// </summary>
    /// <returns>The publisher.</returns>
    /// <param name="waitTime">Wait time.</param>
    private IEnumerator StartPublisher(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            base.Start();
            break;
        }
    }
    /// <summary>
    /// Publishs the message.
    /// </summary>
    /// <param name="message">Message.</param>
    public void PublishMessage(RosSharp.RosBridgeClient.Messages.Roboy.Pose message)
    {
        Publish(message);       
    }
}
