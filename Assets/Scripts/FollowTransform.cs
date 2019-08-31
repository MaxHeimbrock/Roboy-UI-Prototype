using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public bool followPosition = true;
    public bool followYRotation = false;

    public Transform followedTransform;

    public Vector3 offsetPosition;

    public Vector3 offsetRotation;

    // Update is called once per frame
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
