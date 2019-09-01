using System.Collections;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Roboy;

public class MotorStatusSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus>
{
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

    protected override void ReceiveMessage(RosSharp.RosBridgeClient.Messages.Roboy.MotorStatus message)
    {
        Debug.Log("ID: " + message.id);
        Debug.Log("Power: " + message.power_sense);
        Debug.Log("PWM: " + message.pwm_ref);
    }
}
