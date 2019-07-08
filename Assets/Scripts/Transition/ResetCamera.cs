using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ResetCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Valve.VR.OpenVR.System.ResetSeatedZeroPose();
        Valve.VR.OpenVR.Compositor.SetTrackingSpace(
        Valve.VR.ETrackingUniverseOrigin.TrackingUniverseSeated);
    }

    // Update is called once per frame
    void Update()
    {
        InputTracking.disablePositionalTracking = true;
    }
}
