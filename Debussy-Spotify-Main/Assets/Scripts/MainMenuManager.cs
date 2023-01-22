using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    public AudioListener audioListener;
    public GameObject eventSystem;
    public RhythmMenuHandler rhythmMenu;
    private void Awake()
    {
        Instance = this;
    }
    public void upRhythmMenu()
    {
        rhythmMenu.updateCurrentSelection();
    }
    public void activateLoadAdditionalAsync()
    {
        eventSystem.SetActive(false);
        audioListener.enabled = false;
        AudioManager.PauseMusic();
    }
}
