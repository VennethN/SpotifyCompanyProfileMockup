using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionEvents : MonoBehaviour
{
    public Animator transitionAnimator;
    public TransitionDelegate transitionEvent;
    // Start is called before the first frame update
    public void initTransition(string transitionTrigger, TransitionDelegate transEvent)
    {
        transitionEvent = transEvent;
        transitionAnimator.SetTrigger(transitionTrigger);
    }
    public void initEvent()
    {
        transitionEvent();
    }
}
