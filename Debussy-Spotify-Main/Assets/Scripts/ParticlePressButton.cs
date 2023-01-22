using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePressButton : MonoBehaviour
{
    public ParticleSystem pSystem;
    public string soundName;
    public void onClick()
    {
        pSystem.Play();
        if(soundName != null && soundName != "")
        {
            AudioManager.PlayAudioEffect(soundName);
        }
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
