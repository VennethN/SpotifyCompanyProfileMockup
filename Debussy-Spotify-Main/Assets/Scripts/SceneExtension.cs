using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneExtension : MonoBehaviour
{
    public int currentSceneToTransition;
    public void loadSceneOverride(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    public void loadSceneFromIndex()
    {
        SceneManager.LoadScene(currentSceneToTransition);
    }
    public void loadSceneTransition(string transitionName)
    {
        TransitionHandler.Instance.initTransition(transitionName, loadSceneFromIndex);
    }
}
