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
        Debug.Log("start pos: " + localStartPos);
        activeCollisions = 0;
    }

    // Update is called once per frame
    void Update()
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
        Debug.Log("I was hit by " + collision.gameObject.name);
        Debug.Log(collision.relativeVelocity);
        activeCollisions++;
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " stopped hitting me");
        activeCollisions--;
    }
}
