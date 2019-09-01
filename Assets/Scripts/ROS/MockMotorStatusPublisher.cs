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
            break;
        }
    }
    public void PublishMotorMessage()
    {
        // TEST MESSAGE FOR MOTOR STATUS      
        RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus message = new RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus();
        message.id = 0;
        message.power_sense = true;
        message.pwm_ref = new int[] { 10 };
        message.position = new int[] { 2 };
        message.velocity = new int[] { 20 };
        message.displacement = new int[] { 0 };
        message.current = new short[] { 43 };
        message.angle = new int[] { 20 };

        // TEST MESSAGE FOR MOTOR STATUS      
        RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus message_0 = new RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus();
        message_0.id = 1;
        message_0.power_sense = true;
        message_0.pwm_ref = new int[] { 10 };
        message_0.position = new int[] { 2 };
        message_0.velocity = new int[] { 20 };
        message_0.displacement = new int[] { 0 };
        message_0.current = new short[] { 2 };
        message_0.angle = new int[] { 20 };

        PublishMessage(message);
        PublishMessage(message_0);
    }

    private void PublishMessage(RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus message)
    {
        Publish(message);
    }
}
