using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionChangeState : Singleton<TransitionChangeState>
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            StateManager.Instance.GoToNextState();
        }
    }

    public void StartTransitionToHUD()
    {
        animator.SetBool("ToHUD", true);
        animator.SetTrigger("StartTransitionAnimation");
    }

    public void StartTransitionToAdvancedMenu()
    {
        animator.SetBool("ToHUD", false);
        animator.SetTrigger("StartTransitionAnimation");
    }
}
