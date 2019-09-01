using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;

public class MockPosePublisher : Publisher<RosSharp.RosBridgeClient.Messages.Roboy.Pose>
{
    protected override void Start()
    {
        StartCoroutine(StartPublisher(1.0f));
    }

    private IEnumerator StartPublisher(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            base.Start();
            break;
        }
    }

    public void PublishMessage(RosSharp.RosBridgeClient.Messages.Roboy.Pose message)
    {
        Publish(message);       
        //gameObject.GetComponent<PoseSubscriber>().receive(message);
    }
}
