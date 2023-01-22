using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SFB;
static class ListExtensions
    {
    public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
        return list;
    }
}
public class CustomMusicContainer : MonoBehaviour
{
    public int musicIndex { get { return transform.GetSiblingIndex(); } }
    public TMP_InputField musicNameText;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject invalidPathWarning;
    public TMP_InputField musicFilePathText;
    private CustomMusicData cmD;
    public void updateData(CustomMusicData cmDs)
    {
        cmD = cmDs;
        musicNameText.text = cmD.customMusicTitle;
        musicFilePathText.text = cmD.customMusicPath;
        if(musicIndex == 0)
        {
            upButton.SetActive(false);
        }
        else
        {
            upButton.SetActive(true);
        }
        if(musicIndex == GameDataManager.Instance.getCustomMusicDatas().Count-1)
        {
            downButton.SetActive(false);
        }
        else
        {
            downButton.SetActive(true);
        }
        updateValidate(cmD);
    }
    public void updateValidate(CustomMusicData cmD)
    {
        if (CustomMusicHandler.ValidateValidMusicPath(cmD.customMusicPath))
        {
            invalidPathWarning.SetActive(false);
        }
        else
        {
            invalidPathWarning.SetActive(true);
        }
    }
    public void removeMusicData()
    {
        GameDataManager.Instance.removeCustomMusicData(musicIndex);
        CustomMusicHandler.Instance.updateUI();
        CustomMusicHandler.Instance.updateDropdownChoices();
    }
    public void swapUp()
    {
        GameDataManager.Instance.getCustomMusicDatas().Swap<CustomMusicData>(musicIndex, musicIndex - 1);
        CustomMusicHandler.Instance.updateDropdownChoices();
        CustomMusicHandler.Instance.updateUI();
    }
    public void swapDown()
    {
        GameDataManager.Instance.getCustomMusicDatas().Swap<CustomMusicData>(musicIndex, musicIndex + 1);
        CustomMusicHandler.Instance.updateDropdownChoices();
        CustomMusicHandler.Instance.updateUI();
    }
    public void tryChangePath()
    {
        var extensions = new[] {
                new ExtensionFilter("Sound Files", "mp3", "wav" ),
            };
        var path = StandaloneFileBrowser.OpenFilePanel("Choose an audio file", "", extensions, false);
        if (path.Length <= 0) { return; }
        string cPath = path[0];
        musicFilePathText.text = cPath;
        GameDataManager.Instance.updateCustomMusicPath(musicIndex, cPath);
        updateValidate(cmD);
        CustomMusicHandler.Instance.updateDropdownChoices();
    }
    public void updateMusicTitle(string mTitle)
    {
        GameDataManager.Instance.updateCustomMusicTitle(musicIndex, mTitle);
        CustomMusicHandler.Instance.updateDropdownChoices();
    }
}
