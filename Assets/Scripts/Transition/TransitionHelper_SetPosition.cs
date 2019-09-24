using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class moves the camera back in z direction for the transition using a variable from the animator in LateUpdate, because the Vive tracking wants to keep us at (0, 0, 0), because tracking is set to rotation only during transition.
/// The script works on an offset position and rotation set when the transition is started, and moves the camera back in z in local space by adding it on top of that offset of the local space.
/// </summary>
public class TransitionHelper_SetPosition : MonoBehaviour
{
    public float z;

    private Vector3 offset;

    private Quaternion offsetRotation;

    // Update is called once per frame
    void LateUpdate()
    {
        // use offset position + z translation (set by the animator) rotated around y rotation of offset rotation when transition starts
        Vector3 newPosition = offset + (Quaternion.Euler(0, offsetRotation.eulerAngles.y, 0) * new Vector3(0, 0, z));

        this.transform.localPosition = newPosition;
    }

    /// <summary>
    /// This is the current position when transition started, set by the state manager
    /// </summary>
    /// <param name="offset">offset in position when transition started</param>
    public void SetOffset(Vector3 offset)
    {
        this.offset = offset;
    }

    /// <summary>
    /// This is the current rotation when transition started, set by the state manager
    /// </summary>
    /// <param name="offset">offset in rotation when transition started</param>
    public void SetOffsetRotation(Quaternion offsetRotation)
    {
        this.offsetRotation = offsetRotation;
    }
}
