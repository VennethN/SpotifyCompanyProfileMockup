using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void TransitionDelegate();
public class TransitionHandler : MonoBehaviour
{
    public static TransitionHandler Instance;
    public Camera transitionCamera;
    public TransitionEvents tEvent;
    public Animator transitionAnimator;
    public TransitionDelegate transitionEvent;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(transitionCamera);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void initTransition(string transitionTrigger, TransitionDelegate transEvent)
    {
        tEvent.transitionEvent = transEvent;
        transitionAnimator.SetTrigger(transitionTrigger);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
