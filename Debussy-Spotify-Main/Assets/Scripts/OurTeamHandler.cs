using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OurTeamHandler : MonoBehaviour
{
    public Transform parentHolder;
    public List<EmployeeInfo> employeeInfos;
    public List<Transform> elements;
    public List<Image> elementImages;
    public List<Image> realElementImages;
    public List<TMP_Text> employeeInitials;
    public List<CanvasGroup> elementCanvasGroup;
    private List<Vector3> splineVectors = new List<Vector3>();
    public int currentIndex;
    public GameObject buttonLeft;
    public GameObject buttonRight;
    public TMP_Text titleText;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public float animTime;
    public int xOffset;
    public int yOffset;
    public EmployeeInfo currentIndexed
    {
        get
        {
            return employeeInfos[currentIndex];
        }
    }
    public int maxIndex
    {
        get
        {
            return employeeInfos.Count - 1;
        }
    }
    private void Start()
    {
        for (int i = 0; i < parentHolder.childCount; i++)
        {
            parentHolder.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < employeeInfos.Count; i++)
        {
            elements.Add(parentHolder.GetChild(i));
            elementImages.Add(parentHolder.GetChild(i).GetComponent<Image>());
            employeeInitials.Add(parentHolder.GetChild(i).GetChild(0).GetComponent<TMP_Text>());
            elementCanvasGroup.Add(parentHolder.GetChild(i).GetComponent<CanvasGroup>());
            parentHolder.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = 0; i < employeeInitials.Count; i++)
        {
            employeeInitials[i].text = employeeInfos[i].employeeInitials;
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
            LeanTween.scale(elements[i].gameObject, new Vector3(1 - (.2f * Mathf.Abs(idOffset)), 1 - (.2f * Mathf.Abs(idOffset)), 1), timeNeed).setEase(LeanTweenType.easeInOutExpo);
            LeanTween.TMPalphaText(titleText, 0f, animTime / 2f).setOnComplete(() => { titleText.text = currentIndexed.employeeTitle; LeanTween.TMPalphaText(titleText, 1f, animTime / 3f).setDelay(0.2f); });
            LeanTween.TMPalphaText(nameText, 0f, animTime / 2f).setOnComplete(() => { nameText.text = currentIndexed.employeeName; LeanTween.TMPalphaText(nameText, 1f, animTime / 3f).setDelay(0.2f); });
            LeanTween.TMPalphaText(descriptionText, 0f, animTime / 2f).setOnComplete(() => { descriptionText.text = currentIndexed.employeeDescription; LeanTween.TMPalphaText(descriptionText, 1f, animTime / 3f).setDelay(0.2f); });
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
public class EmployeeInfo
{
    public string employeeName;
    public string employeeTitle;
    [TextArea(15, 20)]
    public string employeeDescription;
    public string employeeInitials;
}
