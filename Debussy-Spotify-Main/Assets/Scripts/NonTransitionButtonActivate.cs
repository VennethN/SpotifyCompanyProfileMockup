using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTransitionButtonActivation : TransitionButton
{
    public List<GameObject> transitionObjectActivate;
    public List<GameObject> transitionObjectDeactivate;
    // Update is called once per frame
    public override void transitionDeleg()
    {
        for (int i = 0; i < transitionObjectActivate.Count; i++)
        {
            transitionObjectActivate[i].SetActive(true);
        }
        for (int i = 0; i < transitionObjectDeactivate.Count; i++)
        {
            transitionObjectDeactivate[i].SetActive(false);
        }
    }
}
