﻿using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

[RequireComponent(typeof(InteractionBehaviour))]
public class CustomSlider : MonoBehaviour
{

    private InteractionBehaviour _initObject;
    private Animator titleAnimator;
    private Animator valueAnimator;
    private TextMesh valueText;
    private GameObject IntersectingObject;

    /*public Vector3 v1 = new Vector3(0,0,0);
    public Vector3 v2 = new Vector3(0, 0, 0);
    public Vector3 v3 = new Vector3(0, 0, 0);
    public Vector3 v4 = new Vector3(0, 0, 0);*/

    /**
     * Initialize variables, important to remain prefab hierarchy
     */
    public void Start()
    {
        _initObject = this.GetComponent<InteractionBehaviour>();
        titleAnimator = this.transform.parent.GetChild(1).GetComponent<Animator>();
        valueAnimator = this.transform.GetChild(1).GetComponent<Animator>();
        valueText = this.transform.GetChild(1).GetComponent<TextMesh>();
    }

    /**
     * Checks if there already is an object controlling the slider at the moment
     * If not, the newly intersecting object gets the control over the slider
     */
    private void OnCollisionEnter(Collision collision)
    {
        if(IntersectingObject == null)
        {
            titleAnimator.SetBool("Collision", true);
            valueAnimator.SetBool("VisibleIntersect", true);
            IntersectingObject = collision.gameObject;
            updateValue(collision);
        }
    }

    /**
     * If there is no object controlling the slider at the moment,
     * the still intersecting/colliding object gets the control over the slider.
     * 
     * If the collision is caused by the object having the control over the slider,
     * then the slider value is updated.
     */
    private void OnCollisionStay(Collision collision)
    {
        if (IntersectingObject == null)
        {
            titleAnimator.SetBool("Collision", true);
            valueAnimator.SetBool("VisibleIntersect", true);
            IntersectingObject = collision.gameObject;
            updateValue(collision);
        }
        else if (IntersectingObject.Equals(collision.gameObject))
        {
            updateValue(collision);
        }
    }

    /**
     * If the object with the control of the slider exits the slider,
     * the control access is removed from this object and becomes available for other intersecting objects.
     */
    void OnCollisionExit(Collision collision)
    {
        if (IntersectingObject != null && IntersectingObject.Equals(collision.gameObject))
        {
            IntersectingObject = null;
            titleAnimator.SetBool("Collision", false);
            valueAnimator.SetBool("VisibleIntersect", false);
        }
    }

    /**
     * The value for the slider gets updated according to the position of the collision.
     * The slider fill object and the value text are updated.
     */
    private void updateValue(Collision collision)
    {
        float minX = 1000000000000000000;
        Vector3 closestPoint = new Vector3(0,0,0);
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.point.x < minX)
            {
                minX = contact.point.x;
                closestPoint = contact.point;
            }
        }
        Vector3 localLeftBorderPoint = transform.localPosition;
        localLeftBorderPoint.y += 1f * transform.localScale.y;
        Vector3 worldLeftBorderPoint = transform.TransformPoint(localLeftBorderPoint);
        Vector3 localRightBorderPoint = transform.localPosition;
        localRightBorderPoint.y += -1f * transform.localScale.y;
        Vector3 worldRightBorderPoint = transform.TransformPoint(localRightBorderPoint);
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
        valueText.text = Mathf.Round(fillPercentage * 100f).ToString() + "%";
        Transform fillTransform = transform.GetChild(0);
        fillTransform.localScale = new Vector3(fillTransform.localScale.x, fillPercentage, fillTransform.localScale.z);
        fillTransform.position = new Vector3((transform.position.x - (totalLength - (totalLength * fillPercentage))/2f)-0.0001f, fillTransform.position.y, fillTransform.position.z);
    }

    /**
     * Sets the boolean variable VisiblePointing for the slider text animator.
     * This variable indicates if the user points with a hand to the slider.
     * In this case the slider value becomes visible.
     * This method is called by the FingerDirectionDetection script attached to the hand models (e.g. RiggedHands)
     */
    public void SetVisiblePointer(bool visiblePointing)
    {
        Debug.Log("Set Variable to: " + visiblePointing);
        valueAnimator.SetBool("VisiblePointing", visiblePointing);
    }

    /*
     * Only for Debug purposes
     */
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(v1, 0.001f);
        Gizmos.color = new Color(255,0,0);
        Gizmos.DrawSphere(v2, 0.0012f);
        Gizmos.color = new Color(0, 255, 0);
        Gizmos.DrawSphere(v3, 0.0014f);
        Gizmos.color = new Color(0, 0, 255);
        Gizmos.DrawSphere(v4, 0.16f);
    }*/
}
