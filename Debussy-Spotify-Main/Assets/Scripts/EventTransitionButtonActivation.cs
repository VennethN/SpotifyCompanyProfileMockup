using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTransitionButtonActivation : MonoBehaviour
{
    public List<GameObject> transitionObjectActivate;
    public List<GameObject> transitionObjectDeactivate;
    private List<IActivateAbleUI> transitionObjectIActivateable = new List<IActivateAbleUI>();
    // Update is called once per frame
    public void Start()
    {
        foreach(GameObject t in transitionObjectActivate)
        {
            transitionObjectIActivateable.Add(t.transform.GetComponent<IActivateAbleUI>());
        }
    }
    public void pressButton()
    {
        transitionDeleg();
    }
    public void transitionDeleg()
    {
        for (int i = 0; i < transitionObjectActivate.Count; i++)
        {
            transitionObjectActivate[i].SetActive(true);
            transitionObjectIActivateable[i].onActivateEvent();
        }
        for (int i = 0; i < transitionObjectDeactivate.Count; i++)
        {
            transitionObjectDeactivate[i].SetActive(false);
        }
    }
}
