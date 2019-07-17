using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/**
 * For this animation to work, the VR headset must follow the camera.
 * 
 * Before starting the animation, the viewport has to be resetted to a seated point, because otherwise the camera is too high.
 * Positional Tracking of the head has to be disabled while the animation is running.
 */
public class ResetCamera : MonoBehaviour
{
    void Start()
    {
        // Sets the zero pose for the seated tracker coordinate system to the current position and yaw of the HMD.
        Valve.VR.OpenVR.System.ResetSeatedZeroPose();


        //Valve.VR.OpenVR.Compositor.SetTrackingSpace(Valve.VR.ETrackingUniverseOrigin.TrackingUniverseSeated);
    }

    void Update()
    {
        //InputTracking.disablePositionalTracking = true;
    }
}
