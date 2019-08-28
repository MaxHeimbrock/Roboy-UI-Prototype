using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public bool follow = true;

    public Transform followedTransform;

    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            this.transform.position = followedTransform.position + offset;
        }
    }
}
