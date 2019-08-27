using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimatorScript : Singleton<CameraAnimatorScript>
{
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // In Update() we synchronize the finished animations with the state machine, so that it triggers the next state
    void Update()
    {        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("InAdvancedMenu") && StateManager.Instance.GetCurrentState() == StateManager.MenuState.transitionToAdvancedMenu)
        {
            StateManager.Instance.GoToNextState();
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("InHUD") && StateManager.Instance.GetCurrentState() == StateManager.MenuState.transitionToHUD)
        {
            StateManager.Instance.GoToNextState();
        }
        
    }

    public void StartTransitionToHUD()
    {
        animator.SetTrigger("TransitionToHUD");
    }

    public void StartTransitionToAdvancedMenu()
    {
        animator.SetTrigger("TransitionToAdvancedMenu");
    }

    public void SetButtonPointed()
    {
        animator.SetBool("ButtonPointed", true);
    }

    public void SetButtonNotPointed()
    {
        animator.SetBool("ButtonPointed", false);
    }

}
