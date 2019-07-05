using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    Animator animator;

    private enum state { active, passive };
    private state currentState = state.passive;

    private float startTimer;
    private float timer;
    private float currentTime = 0.0f;

    public float activeTime = 3.0f;
    public float activationTime = 2f;
    
    private bool pointed = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointed)
            Highlight();

        if (currentState == state.active)
        {
            // Check how long ago last highlight was
            timer = (Time.time - startTimer) / activeTime;

            // If longer than activeTime, deactivate
            if (timer >= 1)
            {
                currentState = state.passive;
                animator.SetBool("Active", false);
                //DeactivateAttatchedElements();
            }
        }

        else if (currentState == state.passive)
        {
            // If last highlight was more than 0.1 seconds in the past, reset timer for continuous activation
            if (Time.time - currentTime > 0.3f)
                startTimer = Time.time;
        }
    }

    public void Highlight()
    {
        // When active, just reset start timer of last highlight to now, so it doesn't deactivate 
        if (currentState == state.active)
            startTimer = Time.time;

        // When passive, count continuous highlight time until longer than activationTime -> then activate 
        else if (currentState == state.passive)
        {
            currentTime = Time.time;

            timer = (currentTime - startTimer) / activationTime;

            if (timer >= 1)
            {
                currentState = state.active;
                animator.SetBool("Active", true);
            }
        }
    }

    public void PointerEnter()
    {
        if (pointed == false)
        {
            pointed = true;
            Debug.Log("Enter");
        }
    }

    public void PointerExit()
    {
        if (pointed == true)
        { 
            pointed = false;
            Debug.Log("Exit");
        }
    }
}
