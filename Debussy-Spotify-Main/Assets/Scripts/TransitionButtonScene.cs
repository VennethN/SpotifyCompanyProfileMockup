using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionButtonScene : TransitionButton
{
    public int sceneBuildIndex;
    public int unloadSceneIndex;
    public LoadSceneMode sceneMode;
    // Update is called once per frame
    public override void transitionDeleg()
    {
        SceneManager.LoadScene(sceneBuildIndex, sceneMode);
    }
    public void unloadSceneAsync()
    {
        SceneManager.UnloadSceneAsync(unloadSceneIndex);
    }
}
