using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExtension : MonoBehaviour
{
    public void playSoundEffect(string audioName)
    {
        AudioManager.PlayAudioEffect(audioName);
    }
    public void changeMusic(string audioName)
    {
        AudioManager.ChangeMusicAudio(audioName);
    }
}
