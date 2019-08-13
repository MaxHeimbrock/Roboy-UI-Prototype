using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionButtonAnimation : Singleton<TransitionButtonAnimation>
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
