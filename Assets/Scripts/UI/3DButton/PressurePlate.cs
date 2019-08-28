using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

[RequireComponent(typeof(InteractionBehaviour))]
public class PressurePlate : MonoBehaviour
{
    private InteractionBehaviour _initObject;

    public Vector3 localStartPos;
    public int activeCollisions;

    // Start is called before the first frame update
    void Start()
    {
        _initObject = GetComponent<InteractionBehaviour>();
        localStartPos = transform.localPosition;
        activeCollisions = 0;
    }

    private void FixedUpdate()
    {
        if (activeCollisions == 0)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, localStartPos, Time.deltaTime * 1.5f);
            if (Mathf.Abs(transform.localPosition.z - localStartPos.z) < 0.005)
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