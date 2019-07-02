using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSlider : MonoBehaviour
{

    private GameObject IntersectingObject;

    public Vector3 v1 = new Vector3(0,0,0);
    public Vector3 v2 = new Vector3(0, 0, 0);
    public Vector3 v3 = new Vector3(0, 0, 0);
    public Vector3 v4 = new Vector3(0, 0, 0);

    private void OnCollisionEnter(Collision collision)
    {
        if(IntersectingObject == null)
        {
            IntersectingObject = collision.gameObject;
            updateValue(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (IntersectingObject == null)
        {
            IntersectingObject = collision.gameObject;
            updateValue(collision);
        }
        else if (IntersectingObject.Equals(collision.gameObject))
        {
            updateValue(collision);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (IntersectingObject != null && IntersectingObject.Equals(collision.gameObject))
        {
            IntersectingObject = null;
        }
    }

    private void updateValue(Collision collision)
    {
        float minX = 1000000000000000000;
        Vector3 closestPoint = new Vector3(0,0,0);
        Debug.Log(collision.contactCount);
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log(minX + " --- " + contact.point.x);
            if (contact.point.x < minX)
            {
                minX = contact.point.x;
                closestPoint = contact.point;
            }
        }
        Debug.Log(closestPoint);
        v3 = closestPoint;
        Vector3 localLeftBorderPoint = transform.localPosition;
        localLeftBorderPoint.y += 1f * transform.localScale.y;
        Vector3 worldLeftBorderPoint = transform.TransformPoint(localLeftBorderPoint);
        Debug.Log("worldLeftBorder; " + worldLeftBorderPoint);
        v1 = worldLeftBorderPoint;
        Vector3 localRightBorderPoint = transform.localPosition;
        localRightBorderPoint.y += -1f * transform.localScale.y;
        Vector3 worldRightBorderPoint = transform.TransformPoint(localRightBorderPoint);
        Debug.Log("worldRightBorder: " + worldRightBorderPoint);
        v2 = worldRightBorderPoint;
        if(closestPoint.x < worldLeftBorderPoint.x)
        {
            closestPoint.x = worldLeftBorderPoint.x;
        }
        else if (closestPoint.x > worldRightBorderPoint.x)
        {
            closestPoint.x = worldRightBorderPoint.x;
        }
        float totalLength = worldRightBorderPoint.x - worldLeftBorderPoint.x;
        float fillPercentage = (closestPoint.x - worldLeftBorderPoint.x) / totalLength;
        Transform fillTransform = transform.GetChild(0);
        fillTransform.localScale = new Vector3(fillTransform.localScale.x, fillPercentage, fillTransform.localScale.z);
        fillTransform.position = new Vector3((transform.position.x - (totalLength - (totalLength * fillPercentage))/2f)-0.0001f, fillTransform.position.y, fillTransform.position.z);
    }

    /*
     * Only for Debug purposes
     */
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(v1, 0.001f);
        Gizmos.color = new Color(255,0,0);
        Gizmos.DrawSphere(v2, 0.0012f);
        Gizmos.color = new Color(0, 255, 0);
        Gizmos.DrawSphere(v3, 0.0014f);
        Gizmos.color = new Color(0, 0, 255);
        Gizmos.DrawSphere(v4, 0.16f);
    }
}
