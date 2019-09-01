using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRoboy : MonoBehaviour
{
    Vector3 defaultPos;
    Quaternion defaultRot;
    SenseGlove_Grabable grab;

    private void Start()
    {
        defaultPos = this.transform.localPosition;
        defaultRot = this.transform.localRotation;
        grab = this.GetComponent<SenseGlove_Grabable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter with: " + other.name);
        if(other.name == "TargetZoneStartTransition")
        {
            Debug.Log("SUCCESS: Go to next State");
            StateManager.Instance.GoToNextState();
            if (grab.EndInteractAllowed())
            {
                Debug.Log("Allowed to stop Interaction. Executing now.");
                grab.EndInteraction(grab.GrabScript, true);
                this.transform.localPosition = defaultPos;
                this.transform.localRotation = defaultRot;
            }
        }
    }
}
