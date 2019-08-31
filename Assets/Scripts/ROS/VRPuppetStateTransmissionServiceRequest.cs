using UnityEngine;
using System;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Services.Roboy;

public class VRPuppetStateTransmissionServiceRequest : Singleton<VRPuppetStateTransmissionServiceRequest> {
    /// <summary>
    /// Calls vrpuppets service in order to move the motors during the transition
    /// </summary>
    public void callService() {
        RosConnector rosConnector = GetComponent<RosConnector>();
        VRPuppetStateTransmissionRequest vrPuppetStateTransmissionRequest = new VRPuppetStateTransmissionRequest(true);
        // we need to use '/vr_puppets/state_transmission' for the demo
        rosConnector.RosSocket.CallService<VRPuppetStateTransmissionRequest, VRPuppetStateTransmissionResponse>("/vr_puppets/state_transmission", ServiceCallHandler, new VRPuppetStateTransmissionRequest(true));
    }

    /// <summary>
    /// Handles the response of the service call
    /// </summary>
    /// <param name="message">incoming message</param>
    private void ServiceCallHandler(VRPuppetStateTransmissionResponse message) {
        Debug.Log("VRPuppet Service answered with " + message.success);
    }
}