using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "palmCollider")
        {
            Debug.Log("Table triggered");
            VRPuppetStateTransmissionServiceRequest.Instance.CallHandService(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {        
        if (other.name == "palmCollider")
        {
            Debug.Log("Table collider left");
            VRPuppetStateTransmissionServiceRequest.Instance.CallHandService(false);

        }
    }
}
