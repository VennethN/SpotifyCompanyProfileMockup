using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUXHandler : MonoBehaviour
{
    public Transform parentHolder;
    public List<UIUXInfo> UIUXInfos;
    public List<Transform> elements;
    public List<Image> elementImages;
    public List<CanvasGroup> elementCanvasGroup;
    public List<RectTransform> elementRectTransform;
    private List<Vector3> splineVectors = new List<Vector3>();
    public int currentIndex;
    public GameObject buttonLeft;
    public GameObject buttonRight;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public float animTime;
    public int xOffset;
    public int yOffset;
    public UIUXInfo currentIndexed
    {
        get
        {
            return UIUXInfos[currentIndex];
        }
    }
    public int maxIndex
    {
        get
        {
            return UIUXInfos.Count-1;
        }
    }
    private void Start()
    {
        for (int i = 0; i < parentHolder.childCount; i++)
        {
            parentHolder.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < UIUXInfos.Count; i++)
        {
            elements.Add(parentHolder.GetChild(i));
            elementImages.Add(parentHolder.GetChild(i).GetComponent<Image>());
            elementRectTransform.Add(parentHolder.GetChild(i).GetComponent<RectTransform>());
            elementImages[i].sprite = UIUXInfos[i].UIUXSprite;
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
            LeanTween.size(elementRectTransform[i], UIUXInfos[i].UIUXSprite.textureRect.size* UIUXInfos[i].UIRelativeScale, timeNeed).setEase(LeanTweenType.easeInOutExpo);
            LeanTween.scale(elements[i].gameObject, new Vector3(1f - (.2f * Mathf.Abs(idOffset)), 1 - (.2f * Mathf.Abs(idOffset)), 1f), timeNeed).setEase(LeanTweenType.easeInOutExpo);
            LeanTween.TMPalphaText(descriptionText, 0f, animTime / 2f).setOnComplete(() => { descriptionText.text = currentIndexed.UIUXdescription; LeanTween.TMPalphaText(descriptionText, 1f, animTime / 3f).setDelay(0.2f); });
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
[System.Serializable]
public class UIUXInfo
{
    public Sprite UIUXSprite;
    [TextArea(15, 20)]
    public string UIUXdescription;
    public float UIRelativeScale;
}
