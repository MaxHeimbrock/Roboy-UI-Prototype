using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;

public class PoseSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.Pose>
{
    public RoboyPoseManager PoseManager;

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
        Debug.Log("ID: " + message.id);
        Debug.Log("Orientation x: " + message.orientation.x);
        Debug.Log("Position x: " + message.position.x);
        PoseManager.msg = message;
        PoseManager.poseUpdated = true;         
    }
}
