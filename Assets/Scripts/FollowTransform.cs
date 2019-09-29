using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class to let GameObject follow the transform of another one, without being a child in the scene graph.
/// Supports transition with offset and y-rotation with offset.
/// </summary>
public class FollowTransform : MonoBehaviour
{
    public bool followPosition = true;
    public bool followYRotation = false;

    [Tooltip("Parent GameObject to be followed")]
    public Transform followedTransform;

    public Vector3 offsetPosition;

    public Vector3 offsetRotation;

    /// <summary>
    /// Updates the position and y-rotation according to followed GameObject and given offsets
    /// </summary>
    void Update()
    {
        if (followPosition)
        {
            this.transform.position = followedTransform.position + offsetPosition;
        }
        if (followYRotation)
        {
            this.transform.rotation = Quaternion.Euler(0 + offsetRotation.x, followedTransform.rotation.eulerAngles.y + offsetRotation.y, 0 + offsetRotation.z);
        }
    }

    public void SetOffsetPosition(Vector3 offsetPosition)
    {
        this.offsetPosition = offsetPosition;
    }
}
