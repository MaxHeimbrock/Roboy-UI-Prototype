using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public bool followPosition = true;
    public bool followRotation = false;

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
        if (followRotation)
        {
            this.transform.rotation = Quaternion.Euler(followedTransform.rotation.eulerAngles + offsetRotation);
        }
    }
}
