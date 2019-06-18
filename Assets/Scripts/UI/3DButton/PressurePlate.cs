using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Vector3 localStartPos;
    public int activeCollisions;

    // Start is called before the first frame update
    void Start()
    {
        localStartPos = transform.localPosition;
        activeCollisions = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeCollisions == 0)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, localStartPos, Time.deltaTime * 1.5f);
            if (Mathf.Abs(transform.localPosition.z - localStartPos.z) < 0.05)
            {
                transform.localPosition = localStartPos;
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        activeCollisions++;
    }

    private void OnCollisionExit(Collision collision)
    {
        activeCollisions--;
    }
}
