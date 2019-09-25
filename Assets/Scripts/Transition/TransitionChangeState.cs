using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionChangeState : Singleton<TransitionChangeState>
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            StateManager.Instance.GoToNextState();
        }
    }

    /// <summary>
    /// Triggers animation from advanced menu to operator menu.
    /// </summary>
    public void StartTransitionToHUD()
    {
        animator.SetBool("ToHUD", true);
        animator.SetTrigger("StartTransitionAnimation");
    }

    /// <summary>
    /// Triggers animation from operator menu to advanced menu.
    /// </summary>
    public void StartTransitionToAdvancedMenu()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        animator.SetBool("ToHUD", false);
        animator.SetTrigger("StartTransitionAnimation");
    }
}