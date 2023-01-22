using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionButtonUnloadScene : TransitionButton
{
    public int unloadSceneIndex;
    // Update is called once per frame
    public override void transitionDeleg()
    {
        SceneManager.UnloadSceneAsync(unloadSceneIndex);
        AudioManager.ResumeMusic();
    }
}
