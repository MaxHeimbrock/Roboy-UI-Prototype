using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;
using Leap.Unity;
using UnityEditor.Events;

[RequireComponent(typeof(InteractionBehaviour))]
public class CustomSlider : MonoBehaviour
{

    private Vector3 defaultPosFull;
    private Vector3 defaultPosFill;

    private InteractionBehaviour _initObject;
    private Animator titleAnimator;
    private Animator valueAnimator;
    private TextMesh valueText;
    private GameObject IntersectingObject;
    private float value;

    //For Debugging only
    /*public Vector3 v1 = new Vector3(0,0,0);
    public Vector3 v2 = new Vector3(0, 0, 0);
    public Vector3 v3 = new Vector3(0, 0, 0);
    public Vector3 v4 = new Vector3(0, 0, 0);*/

    /// <summary>
    /// Initialize variables, dependent on consistent prefab hierarchy.
    /// </summary>
    public void Start()
    {
        _initObject = this.GetComponent<InteractionBehaviour>();
        titleAnimator = this.transform.parent.GetChild(1).GetComponent<Animator>();
        valueAnimator = this.transform.GetChild(1).GetComponent<Animator>();
        valueText = this.transform.GetChild(1).GetComponent<TextMesh>();
        value = 1f;

        defaultPosFull = transform.localPosition;
        defaultPosFill = transform.GetChild(0).localPosition;
    }

    /// <summary>
    /// Checks if there already is an object controlling the slider at the moment
    /// If not, the newly intersecting object gets the control over the slider
    /// </summary>
    /// <param name="collision"></param>
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

    /// <summary>
    /// If there is no object controlling the slider at the moment,
    /// the still intersecting/colliding object gets the control over the slider.
    ///
    /// If the collision is caused by the object having the control over the slider,
    /// then the slider value is updated.
    /// </summary>
    /// <param name="collision"></param>
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

    /// <summary>
    /// If the object with the control of the slider exits the slider,
    /// the control access is removed from this object and becomes available for other intersecting objects.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionExit(Collision collision)
    {
        if (IntersectingObject != null && IntersectingObject.Equals(collision.gameObject))
        {
            IntersectingObject = null;
            titleAnimator.SetBool("Collision", false);
            valueAnimator.SetBool("VisibleIntersect", false);
        }
    }

    /// <summary>
    /// The value for the slider gets updated according to the position of the collision.
    /// The slider fill object and the value text are updated.
    /// </summary>
    /// <param name="collision">The collision of the slider and the object controlling it.</param>
    private void updateValue(Collision collision)
    {
        Transform fillTransform = transform.GetChild(0);
        if(!fillTransform.gameObject.activeSelf)
        {
            fillTransform.gameObject.SetActive(true);
        }

        //Find contact point giving max value
        float maxY = -1000000000000000000;
        Vector3 closestPoint = new Vector3(0,0,0);
        Vector3 contactPointLocal;
        foreach (ContactPoint contact in collision.contacts)
        {
            contactPointLocal = transform.InverseTransformPoint(contact.point);
            if (contactPointLocal.y > maxY)
            {
                maxY = contactPointLocal.y;
                closestPoint = contactPointLocal;
            }
        }

        Vector3 localLeftBorderPoint = transform.localPosition;
        localLeftBorderPoint.y += 1f * transform.localScale.y;
        Vector3 localRightBorderPoint = transform.localPosition;
        localRightBorderPoint.y += -1f * transform.localScale.y;

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
        if(value < 1f)
        {
            if(value > 0f)
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
        fillTransform.localPosition = new Vector3(fillTransform.localPosition.x, (transform.localPosition.y + (totalLength - (totalLength * value))/2f)+0.0001f, fillTransform.localPosition.z);
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

    // Only for debugging
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
