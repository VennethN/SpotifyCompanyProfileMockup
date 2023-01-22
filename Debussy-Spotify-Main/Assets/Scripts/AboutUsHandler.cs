using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AboutUsHandler : MonoBehaviour
{
    public Transform parentHolder;
    public List<Sprite> spriteInfos;
    public List<Transform> elements;
    public List<Image> elementImages;
    public List<CanvasGroup> elementCanvasGroup;
    public List<RectTransform> elementRectTransform;
    public int currentIndex;
    public GameObject buttonLeft;
    public GameObject buttonRight;
    public float animTime;
    public int xOffset;
    public int yOffset;
    public Sprite currentIndexed
    {
        get
        {
            return spriteInfos[currentIndex];
        }
    }
    public int maxIndex
    {
        get
        {
            return spriteInfos.Count - 1;
        }
    }
    private void Start()
    {
        for (int i = 0; i < parentHolder.childCount; i++)
        {
            parentHolder.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < spriteInfos.Count; i++)
        {
            elements.Add(parentHolder.GetChild(i));
            elementImages.Add(parentHolder.GetChild(i).GetComponent<Image>());
            elementRectTransform.Add(parentHolder.GetChild(i).GetComponent<RectTransform>());
            elementImages[i].sprite = spriteInfos[i];
            elementCanvasGroup.Add(parentHolder.GetChild(i).GetComponent<CanvasGroup>());
            parentHolder.GetChild(i).gameObject.SetActive(true);
        }
        updateButtonGraphics();
        setAllPlace(0f);
    }
    public void setAllPlace(float timeNeed)
    {

        for (int i = 0; i < elements.Count; i++)
        {
            int idOffset = i - currentIndex;
            LeanTween.moveLocal(elements[i].gameObject, new Vector3(0 + (idOffset * xOffset), 0, 0), timeNeed).setEase(LeanTweenType.easeInOutExpo);
            //LeanTween.size(elementRectTransform[i], spriteInfos[i].textureRect.size, timeNeed).setEase(LeanTweenType.easeInOutExpo);
            LeanTween.scale(elements[i].gameObject, new Vector3(1.3f - (.2f * Mathf.Abs(idOffset)), 1.3f - (.2f * Mathf.Abs(idOffset)), 1.3f), timeNeed).setEase(LeanTweenType.easeInOutExpo);
            LeanTween.alphaCanvas(elementCanvasGroup[i], 1 - (.5f * Mathf.Abs(idOffset)), animTime).setEase(LeanTweenType.easeInOutExpo);
        }
    }
    public void updateButtonGraphics()
    {
        buttonLeft.SetActive(true);
        buttonRight.SetActive(true);
        if (currentIndex == 0)
        {
            buttonLeft.SetActive(false);
        }
        if (currentIndex == maxIndex)
        {
            buttonRight.SetActive(false);
        }
    }
    public void moveLeft()
    {
        currentIndex--;
        setAllPlace(animTime);
        updateButtonGraphics();
    }

    public void moveRight()
    {
        currentIndex++;
        setAllPlace(animTime);
        updateButtonGraphics();
    }
    public void getIDScale(int curId)
    {
        int idOffSet = curId - currentIndex;

    }
}
