
using UnityEngine;

using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Services.Roboy;

public class TestServicePublisher: MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {   
            callService();
        }
    }

    /// <summary>
    /// This method publishes a simple string messages to the topic of the object.
    /// </summary>
    /// <param name="message">is the message, which shall be published.</param>
    public void callService() {
        RosConnector rosConnector = GetComponent<RosConnector>();
        
        VRPuppetStateTransmissionRequest vrPuppetStateTransmissionRequest = new VRPuppetStateTransmissionRequest(true);
        rosConnector.RosSocket.CallService<VRPuppetStateTransmissionRequest, VRPuppetStateTransmissionResponse>("/vr_puppets/state_transmission", ServiceCallHandler,
            new VRPuppetStateTransmissionRequest(true));
    }
    private void ServiceCallHandler(VRPuppetStateTransmissionResponse message)
    {
        Debug.Log("ROS Distro: " + message.success);
    }

}