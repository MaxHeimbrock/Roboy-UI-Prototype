using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionButtonAnimation : Singleton<TransitionButtonAnimation>
{
    Animator animator;

    /// <summary>
    /// Sets the reference to the animator.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Call this method when the button is pointed at to start the animation.
    /// </summary>
    public void SetButtonPointed()
    {
        animator.SetBool("ButtonPointed", true);
    }

    /// <summary>
    /// Call this method when nothing is pointing at the button anymore to stop the animation.
    /// </summary>
    public void SetButtonNotPointed()
    {
        animator.SetBool("ButtonPointed", false);
    }
}
