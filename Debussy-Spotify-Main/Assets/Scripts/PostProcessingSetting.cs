using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PostProcessingSetting : MonoBehaviour
{
    public Transform buttonKnob;
    public Transform onPosition;
    public Transform offPosition;
    public Image knobImage;
    public float timeNeed;
    private void Start()
    {
        snapButtonState();
    }
    public void onClick()
    {
        VolumeManager.Instance.volume.gameObject.SetActive(!VolumeManager.Instance.volume.gameObject.activeSelf);
        updateButtonState();
    }
    public void snapButtonState()
    {
        if (VolumeManager.Instance.volume.gameObject.activeSelf)
        {
            LeanTween.move(buttonKnob.gameObject, onPosition.position, 0).setEase(LeanTweenType.easeInOutExpo);
            LeanTween.value(buttonKnob.gameObject, setColorCallback, knobImage.color, Color.green, 0).setEase(LeanTweenType.easeInOutExpo);
        }
        else
        {
            LeanTween.move(buttonKnob.gameObject, offPosition.position, 0).setEase(LeanTweenType.easeInOutExpo);
            LeanTween.value(buttonKnob.gameObject, setColorCallback, knobImage.color, Color.red, 0).setEase(LeanTweenType.easeInOutExpo);
        }
    }
    public void updateButtonState()
    {
        if (VolumeManager.Instance.volume.gameObject.activeSelf)
        {
            LeanTween.move(buttonKnob.gameObject, onPosition.position, timeNeed).setEase(LeanTweenType.easeInOutExpo);
            LeanTween.value(buttonKnob.gameObject, setColorCallback,knobImage.color,Color.green, timeNeed).setEase(LeanTweenType.easeInOutExpo);
        }
        else
        {
            LeanTween.move(buttonKnob.gameObject, offPosition.position, timeNeed).setEase(LeanTweenType.easeInOutExpo);
            LeanTween.value(buttonKnob.gameObject, setColorCallback, knobImage.color, Color.red, timeNeed).setEase(LeanTweenType.easeInOutExpo);
        }
    }
    public void setColorCallback(Color c)
    {
        knobImage.color = c;

        // For some reason it also tweens my image's alpha so to set alpha back to 1 (I have my color set from inspector). You can use the following

        var tempColor = knobImage.color;
        tempColor.a = 1f;
        knobImage.color = tempColor;
    }
}
