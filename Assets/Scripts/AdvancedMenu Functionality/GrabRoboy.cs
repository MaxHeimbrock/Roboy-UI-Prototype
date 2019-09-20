using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRoboy : MonoBehaviour
{
    #region properties

    Vector3 defaultPos;
    Quaternion defaultRot;
    SenseGlove_Grabable grab;

    #endregion

    /// <summary>
    /// Store initial position and orientation.
    /// Get the reference to Grabable script.
    /// </summary>
    private void Start()
    {
        defaultPos = this.transform.localPosition;
        defaultRot = this.transform.localRotation;
        grab = this.GetComponent<SenseGlove_Grabable>();
    }

    /// <summary>
    /// If this object (aka Roboy's head) is pulled close enough,
    /// the transition to the HUD gets initiated and this object's position and orientation are resetted
    /// </summary>
    /// <param name="other">The other collider this object starts intersecting with</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "TargetZoneStartTransition")
        {
            StateManager.Instance.GoToNextState();
            if (grab.EndInteractAllowed())
            {
                grab.EndInteraction(grab.GrabScript, true);
                this.transform.localPosition = defaultPos;
                this.transform.localRotation = defaultRot;
            }
        }
    }
}
