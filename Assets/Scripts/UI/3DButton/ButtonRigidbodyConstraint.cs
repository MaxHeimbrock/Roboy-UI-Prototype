using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ButtonRigidbodyConstraint : MonoBehaviour
{
    private Vector3 defaultPos;
    private Quaternion defaultRot;

    /// <summary>
    /// Store the initial position and orientation. 
    /// </summary>
    private void Start()
    {
        defaultPos = this.transform.localPosition;
        defaultRot = this.transform.localRotation;
    }

    /// <summary>
    /// Constrain this object to movement along the local z axis only.
    /// </summary>
    private void Update()
    {
        this.transform.localRotation = defaultRot;
        this.transform.localPosition = new Vector3(defaultPos.x, defaultPos.y, this.transform.localPosition.z);
    }
}
