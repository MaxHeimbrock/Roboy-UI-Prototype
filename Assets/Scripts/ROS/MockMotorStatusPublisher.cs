using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

public class MockMotorStatusPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus>
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

            // TEST MESSAGE FOR MOTOR STATUS      
            RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus message = new RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus();
            message.id = 0;
            message.power_sense = true;
            message.pwm_ref = new int[] {10};
            message.position = new int[] {2};
            message.velocity = new int[] {20};
            message.displacement = new int[] {0};
            message.current = new short[] {2};
            message.angle = new int[] {20};
            PublishMessage(message);

             // TEST MESSAGE FOR MOTOR STATUS      
            RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus message_0 = new RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus();
            message.id = 1;
            message.power_sense = true;
            message.pwm_ref = new int[] {10};
            message.position = new int[] {2};
            message.velocity = new int[] {20};
            message.displacement = new int[] {0};
            message.current = new short[] {2};
            message.angle = new int[] {20};

            PublishMessage(message);
            yield return new WaitForSeconds(waitTime);
            PublishMessage(message_0);
            break;
        }
    }

    private void PublishMessage(RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus message)
    {
        Publish(message);
    }
}
