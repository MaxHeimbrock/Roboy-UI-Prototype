using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;

public class PoseSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.Pose>
{
    public RoboyPoseManager PoseManager;
    public GameObject Roboy;

    protected override void Start()
    {
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
        PoseManager.UpdatePose(message);
    }

    public void receive(RosSharp.RosBridgeClient.Messages.Roboy.Pose message)
    {
        ReceiveMessage(message);
    }
}
