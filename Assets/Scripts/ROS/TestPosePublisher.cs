using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

public class TestPosePublisher : Publisher<RosSharp.RosBridgeClient.Messages.Roboy.Pose>
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

            // TEST MESSAGE FOR RIGHT HAND UPDATE           
            RosSharp.RosBridgeClient.Messages.Roboy.Pose message = new RosSharp.RosBridgeClient.Messages.Roboy.Pose();
            message.id = 2;
            // Conversion between ROS Quaternion and Unity Quaternion
            RosSharp.RosBridgeClient.Messages.Geometry.Quaternion or = new RosSharp.RosBridgeClient.Messages.Geometry.Quaternion();
            Quaternion currentOr = Quaternion.Euler((float)-3.353, (float)-148.248, (float)-12.106);
            or.x = currentOr.x;
            or.y = currentOr.y;
            or.z = currentOr.z;
            or.w = currentOr.w;
            message.orientation = or;
            // Conversion between ROS Point and Unity Vector3
            RosSharp.RosBridgeClient.Messages.Geometry.Point po = new RosSharp.RosBridgeClient.Messages.Geometry.Point();
            po.x = (float)-0.514;
            po.y = (float)-0.05;
            po.z = (float)-0.552;
            message.position = po;
            // Update position after a couple of seconds
            yield return new WaitForSeconds(waitTime);
            PublishMessage(message);
            break;
        }
    }

    private void PublishMessage(RosSharp.RosBridgeClient.Messages.Roboy.Pose message)
    {
        Publish(message);
    }
}
