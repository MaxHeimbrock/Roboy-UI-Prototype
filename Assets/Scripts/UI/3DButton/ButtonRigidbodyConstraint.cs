using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ButtonRigidbodyConstraint : MonoBehaviour
{
    private Vector3 defaultPos;
    private Quaternion defaultRot;

    private void Start()
    {
        defaultPos = this.transform.localPosition;
        defaultRot = this.transform.localRotation;
    }

    private void Update()
    {
        this.transform.localRotation = defaultRot;
        this.transform.localPosition = new Vector3(defaultPos.x, defaultPos.y, this.transform.localPosition.z);
    }
}
