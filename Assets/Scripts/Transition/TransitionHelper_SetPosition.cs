using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHelper_SetPosition : MonoBehaviour
{
    public float z;

    private Vector3 offset;

    private Quaternion offsetRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // use offset position + z translation (set by the animator) rotated around y rotation of offset rotation when transition starts
        Vector3 newPosition = offset + (Quaternion.Euler(0, offsetRotation.eulerAngles.y, 0) * new Vector3(0, 0, z));

        this.transform.localPosition = newPosition;
    }

    public void SetOffset(Vector3 offset)
    {
        this.offset = offset;
    }

    public void SetOffsetRotation(Quaternion offsetRotation)
    {
        this.offsetRotation = offsetRotation;
    }
}
