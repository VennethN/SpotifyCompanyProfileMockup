using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionButton : MonoBehaviour
{
    public string transitionTrigger;
    public virtual void onClickActivateTransition()
    {
        TransitionHandler.Instance.initTransition(transitionTrigger, transitionDeleg);
    }
    public virtual void onClickActivateWithoutTransition()
    {
        transitionDeleg();
    }
    public virtual void transitionDeleg()
    {
        
    }
}
