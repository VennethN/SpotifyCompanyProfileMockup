using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour, IPoolAble
{
    public AudioSource audioSource;
    public void InitiateAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(disableDelay());

    }
    IEnumerator disableDelay()
    {
        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);
        gameObject.SetActive(false);
    }
    public void OnEnable()
    {

    }
    public void OnDisable()
    {
        ObjectPool.ReturnGameObject(this);
    }
}
