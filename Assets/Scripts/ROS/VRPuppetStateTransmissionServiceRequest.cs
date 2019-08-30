using UnityEngine;
using System;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Services.Roboy;

public class VRPuppetStateTransmissionServiceRequest : Singleton<VRPuppetStateTransmissionServiceRequest>
{
    public void Start()
    {
        /* For testing purposes, comment OUT for FINAL DEMO */
        //Advertise the service
        RosConnector rosConnector = GetComponent<RosConnector>();
        rosConnector.RosSocket.AdvertiseService<VRPuppetStateTransmissionRequest, VRPuppetStateTransmissionResponse>("/roboy_middleware_msgs/VRPuppetStateTransmission", ServiceResponseHandler);
    }

    public void callService()
    {
        // Call the service VRPuppetStateTransmission when transitioning to Advanced Menu

        RosConnector rosConnector = GetComponent<RosConnector>();
        VRPuppetStateTransmissionRequest vrPuppetStateTransmissionRequest = new VRPuppetStateTransmissionRequest(true);
        rosConnector.RosSocket.CallService<VRPuppetStateTransmissionRequest, VRPuppetStateTransmissionResponse>("/roboy_middleware_msgs/VRPuppetStateTransmission", ServiceCallHandler,
            new VRPuppetStateTransmissionRequest(true));
    }

    // Mock handler for receiving requests
    private void ServiceCallHandler(VRPuppetStateTransmissionResponse message)
    {
        Debug.Log("Message is " + message.success);
        Debug.Log("Message: " + message.message);
    }

    // Example for sending a response to a call 
      private static bool ServiceResponseHandler(VRPuppetStateTransmissionRequest arguments, out VRPuppetStateTransmissionResponse result)
    {
        result = new VRPuppetStateTransmissionResponse(true, "transition init");       
        return true;
    }
}
