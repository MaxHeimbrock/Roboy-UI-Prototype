using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCorner : UI_Element
{
    public Transform bottomMenuTransform;
    Quaternion bottom = Quaternion.Euler(new Vector3(20, 0, 0));
    Quaternion top = Quaternion.Euler(new Vector3(7, 0, 0));
    Vector3 front = new Vector3(0, -0.15f, 0.8f);
    Vector3 back = new Vector3(0, -0.15f, 1.2f);

    enum state {bottom, highlighted, movingUp, top, movingDown};
    state currentState = state.bottom;

    public float highlightTime = 0.5f;
    public float rotationTime = 0.5f;
    public float topTime = 3.0f;

    // The time at which the animation started.
    private float startTime;
    private float currentTime = 0.0f;
    private float fracComplete;

    int highlighted = 0;

    public override void Highlight()
    {
        highlighted = 1;
    }

    protected override void SubclassStart()
    {

    }

    protected override void SubclassUpdate()
    {
        // Still highlighted 1 to 5 frames ago
        if (highlighted > 0)
        {
            switch (currentState)
            {
                // Start the highlighting phase with timer from bottom phase
                case state.bottom:
                    startTime = Time.time;
                    currentState = state.highlighted;
                    break;
                // If highlighting phase is not broken by a reset of startTime, move to movingUp phase
                case state.highlighted:
                    if (Time.time - startTime >= highlightTime)
                    {
                        currentState = state.movingUp;
                        startTime = Time.time;
                        this.transform.localPosition = back;
                    }
                    break;
                // Slerp rotation and set back magic corner until fracComplete is bigger than 1 - same wether highlighted or not
                case state.movingUp:
                    fracComplete = (Time.time - startTime) / rotationTime;
                    // Still rotating
                    if (fracComplete < 1.0f)
                        bottomMenuTransform.localRotation = Quaternion.Slerp(bottom, top, fracComplete);
                    // Rotation done - go to next state
                    else
                    {
                        currentState = state.top;
                        startTime = Time.time;
                        //Debug.Log("Up");
                    }
                    break;
                // If another highlight while on top, reset top timer
                case state.top:
                    startTime = Time.time;
                    break;
                // Slerp rotation and set forward magic corner until fracComplete is bigger than 1 - same wether highlighted or not
                case state.movingDown:
                    fracComplete = (Time.time - startTime) / rotationTime;
                    // Still rotating
                    if (fracComplete < 1.0f)
                        bottomMenuTransform.localRotation = Quaternion.Slerp(top, bottom, fracComplete);
                    // Rotation done - go to next state
                    else
                    {
                        currentState = state.bottom;
                        startTime = Time.time;
                        this.transform.localPosition = front;
                        //Debug.Log("Down");
                    }
                    break;
            }

            // reduce highlighted frames timer
            highlighted--;
        }
        // Last highlight was more than 5 frames ago
        else
        {
            switch (currentState)
            {
                case state.bottom:
                    break;
                case state.highlighted:
                    startTime = Time.time;
                    currentState = state.bottom;
                    break;
                // Slerp rotation and set back magic corner until fracComplete is bigger than 1 - same wether highlighted or not
                case state.movingUp:
                    fracComplete = (Time.time - startTime) / rotationTime;
                    // Still rotating
                    if (fracComplete < 1.0f)
                        bottomMenuTransform.localRotation = Quaternion.Slerp(bottom, top, fracComplete);
                    // Rotation done - go to next state
                    else
                    {
                        currentState = state.top;
                        startTime = Time.time;
                        //Debug.Log("Up");
                    }
                    break;
                // Count time on top
                case state.top:
                    if (Time.time - startTime > topTime)
                    {
                        currentState = state.movingDown;
                        startTime = Time.time;
                    }
                    break;
                // Slerp rotation and set forward magic corner until fracComplete is bigger than 1 - same wether highlighted or not
                case state.movingDown:
                    fracComplete = (Time.time - startTime) / rotationTime;
                    // Still rotating
                    if (fracComplete < 1.0f)
                        bottomMenuTransform.localRotation = Quaternion.Slerp(top, bottom, fracComplete);
                    // Rotation done - go to next state
                    else
                    {
                        currentState = state.bottom;
                        startTime = Time.time;
                        this.transform.localPosition = front;
                        //Debug.Log("Down");
                    }
                    break;
            }
        }
    }
}
