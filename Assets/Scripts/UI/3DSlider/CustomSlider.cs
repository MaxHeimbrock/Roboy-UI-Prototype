﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;

public class CustomSlider : MonoBehaviour
{
    #region properties 

    public float defaultValue;
    private int[] fingers;

    private Vector3 defaultPosFull;
    private Animator titleAnimator;
    private Animator valueAnimator;
    private TextMesh valueText;
    private GameObject IntersectingObject;
    private float value;

    #endregion

    /// <summary>
    /// As reset set the slider's default value to 100%
    /// </summary>
    private void Reset()
    {
        defaultValue = 100f;
    }

    /// <summary>
    /// Initialize variables, dependent on consistent prefab hierarchy.
    /// </summary>
    public void Start()
    {
        titleAnimator = this.transform.parent.GetChild(1).GetComponent<Animator>();
        valueAnimator = this.transform.GetChild(1).GetComponent<Animator>();
        valueText = this.transform.GetChild(1).GetComponent<TextMesh>();

        fingers = new[] { 0, 0, 0, 0, 0 };

        defaultPosFull = transform.localPosition;

        setDefaultValue();
    }

    /// <summary>
    /// If requested, activate the buzz motor for the respective finger.
    /// </summary>
    private void Update()
    {
        for(int i = 0; i < fingers.Length; i++)
        {
            if(fingers[i] > 0)
            {
                BuzzManager.Instance.ActivateFinger(i, fingers[i]);
            }
        }
    }

    #region Collision handling

    /// <summary>
    /// Checks if there already is an object controlling the slider at the moment
    /// If not, the newly intersecting object gets the control over the slider
    /// </summary>
    /// <param name="collision">The other collider this object starts intersecting with.</param>
    private void OnTriggerEnter(Collider collision)
    {
        if (IntersectingObject == null && collision.gameObject.CompareTag("HandMatchingCollider"))//colliders.Contains(collision.name))
        {
            titleAnimator.SetBool("Collision", true);
            valueAnimator.SetBool("VisibleIntersect", true);
            IntersectingObject = collision.gameObject;
            updateValue(collision.transform.position);
        }
        if (collision.gameObject.CompareTag("HandMatchingCollider"))
        {
            if (collision.name.Equals("thumbMatchCollider"))
            {
                fingers[0] = 50;
            }
            if (collision.name.Equals("indexMatchCollider"))
            {
                fingers[1] = 50;
            }
            if (collision.name.Equals("middleMatchCollider"))
            {
                fingers[2] = 50;
            }
            if (collision.name.Equals("ringMatchCollider"))
            {
                fingers[3] = 50;
            }
            if (collision.name.Equals("pinkyMatchCollider"))
            {
                fingers[4] = 50;
            }
        }
    }

    /// <summary>
    /// If there is no object controlling the slider at the moment,
    /// the still intersecting/colliding object gets the control over the slider.
    ///
    /// If the collision is caused by the object having the control over the slider,
    /// then the slider value is updated.
    /// </summary>
    /// <param name="collision">The other collider this object keeps intersecting with.</param>
    private void OnTriggerStay(Collider collision)
    {
        if (IntersectingObject == null && collision.gameObject.CompareTag("HandMatchingCollider"))
        {
            titleAnimator.SetBool("Collision", true);
            valueAnimator.SetBool("VisibleIntersect", true);
            IntersectingObject = collision.gameObject;
            updateValue(collision.transform.position);
        }
        else if (IntersectingObject != null && IntersectingObject.Equals(collision.gameObject))
        {
            updateValue(collision.transform.position);
        }
    }

    /// <summary>
    /// If the object with the control of the slider exits the slider,
    /// the control access is removed from this object and becomes available for other intersecting objects.
    /// </summary>
    /// <param name="collision">The other collider this object stops intersecting with.</param>
    void OnTriggerExit(Collider collision)
    {
        if (IntersectingObject != null && IntersectingObject.Equals(collision.gameObject))
        {
            IntersectingObject = null;
            titleAnimator.SetBool("Collision", false);
            valueAnimator.SetBool("VisibleIntersect", false);
        }
        if (collision.gameObject.CompareTag("HandMatchingCollider"))
        {
            if (collision.name.Equals("thumbMatchCollider"))
            {
                fingers[0] = 0;
            }
            if (collision.name.Equals("indexMatchCollider"))
            {
                fingers[1] = 0;
            }
            if (collision.name.Equals("middleMatchCollider"))
            {
                fingers[2] = 0;
            }
            if (collision.name.Equals("ringMatchCollider"))
            {
                fingers[3] = 0;
            }
            if (collision.name.Equals("pinkyMatchCollider"))
            {
                fingers[4] = 0;
            }
        }
    }

    #endregion

    /// <summary>
    /// The value for the slider gets updated according to the position of the collision.
    /// The slider fill object and the value text are updated.
    /// </summary>
    /// <param name="worldPoint">Intersection point of slider and controlling object.</param>
    private void updateValue(Vector3 worldPoint)
    {
        Vector3 localRightBorderPoint = transform.localPosition;
        localRightBorderPoint.y += -1f * transform.localScale.y;
        Transform fillTransform = transform.GetChild(0);
        if (!fillTransform.gameObject.activeSelf)
        {
            fillTransform.gameObject.SetActive(true);
        }

        Vector3 closestPoint = transform.InverseTransformPoint(worldPoint);
        Vector3 localLeftBorderPoint = transform.localPosition;
        localLeftBorderPoint.y += 1f * transform.localScale.y;

        //Trim to slider size
        if (closestPoint.y > localLeftBorderPoint.y)
        {
            closestPoint.y = localLeftBorderPoint.y;
        }
        else if (closestPoint.y < localRightBorderPoint.y)
        {
            closestPoint.y = localRightBorderPoint.y;
        }

        //Slider value in percentage
        float totalLength = localLeftBorderPoint.y - localRightBorderPoint.y;
        value = (localLeftBorderPoint.y - closestPoint.y) / totalLength;
        valueText.text = Mathf.Round(value * 100f).ToString() + "%";

        //Apply new value: scale & position
        if (value < 1f)
        {
            if (value > 0f)
            {
                fillTransform.localScale = new Vector3(fillTransform.localScale.x, value, fillTransform.localScale.z);
            }
            else
            {
                fillTransform.gameObject.SetActive(false);
            }
        }
        else
        {
            fillTransform.localScale = new Vector3(fillTransform.localScale.x, value + 0.0002f, fillTransform.localScale.z);
        }
        fillTransform.localPosition = new Vector3(fillTransform.localPosition.x, (transform.localPosition.y + (totalLength - (totalLength * value)) / 2f) + 0.0001f, fillTransform.localPosition.z);
    }

    /// <summary>
    /// Returns the current value for the slider.
    /// </summary>
    /// <returns>Slider value</returns>
    public float GetValue()
    {
        return value;
    }

    /// <summary>
    /// Updates the slider to the default position.
    /// </summary>
    public void setDefaultValue()
    {
        Vector3 localLeftBorderPoint = transform.localPosition;
        localLeftBorderPoint.y += 1f * transform.localScale.y;
        Vector3 localRightBorderPoint = transform.localPosition;
        localRightBorderPoint.y += -1f * transform.localScale.y;
        Debug.Log("Called SetDefaultValue, updating slider now " + defaultValue);
        updateValue(transform.TransformPoint(localLeftBorderPoint - new Vector3(0, (defaultValue / 100f) * (localLeftBorderPoint.y - localRightBorderPoint.y), 0)));
    }

    /// <summary>
    /// Sets the boolean variable VisiblePointing for the slider text animator.
    /// This variable indicates if the user points with a hand to the slider.
    /// In this case the slider value becomes visible.
    /// This method is called by the FingerDirectionDetection script attached to the hand models(e.g.RiggedHands)
    /// </summary>
    /// <param name="visiblePointing"></param>
    public void SetVisiblePointer(bool visiblePointing)
    {
        valueAnimator.SetBool("VisiblePointing", visiblePointing);
    }

    /// <summary>
    /// Returns slider to the position saved at the start.
    /// </summary>
    public void ReturnToDefaultPos()
    {
        transform.localPosition = defaultPosFull;
    }
}