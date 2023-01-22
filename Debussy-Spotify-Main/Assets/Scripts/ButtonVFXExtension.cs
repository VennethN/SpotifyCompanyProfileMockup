using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVFXExtension : MonoBehaviour
{
    public void spawnParticle(string partName)
    {
        GameVFXManager.SpawnParticle(partName, transform.position);
    }
    public void spawnParticleMouse(string partName)
    {
        GameVFXManager.SpawnParticle(partName);
    }

    public void playSound(string soundName)
    {
        AudioManager.PlayAudioEffect(soundName);
    }
}
