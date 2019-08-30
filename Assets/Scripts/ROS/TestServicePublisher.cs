
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

    public void callService() {
        RosConnector rosConnector = GetComponent<RosConnector>();
        
        VRPuppetStateTransmissionRequest vrPuppetStateTransmissionRequest = new VRPuppetStateTransmissionRequest(true);
        rosConnector.RosSocket.CallService<VRPuppetStateTransmissionRequest, VRPuppetStateTransmissionResponse>("/roboy_middleware_msgs/VRPuppetStateTransmission", ServiceCallHandler,
            new VRPuppetStateTransmissionRequest(true));
    }
    private void ServiceCallHandler(VRPuppetStateTransmissionResponse message)
    {
        Debug.Log("ROS Distro: " + message.success);
    }

}