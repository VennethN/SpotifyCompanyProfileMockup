using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroHandler : MonoBehaviour
{
    public Animator introAnimtr;
    public Animator introAnimtr2;
    public GameObject anmtr2;
    public bool allowToContinue;
    public void allowC()
    {
        allowToContinue = true;
    }
    public void continueAnimator()
    {
        anmtr2.SetActive(true);
    }
    private void Update()
    {

        if(Input.GetMouseButtonDown(0) && allowToContinue)
        {
            if(EventSystem.current.IsPointerOverGameObject()) { return; }
            allowToContinue = !allowToContinue;
            introAnimtr.SetTrigger("Continue");
        }
    }
}
