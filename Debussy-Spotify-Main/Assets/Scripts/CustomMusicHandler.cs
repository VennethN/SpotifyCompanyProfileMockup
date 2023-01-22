using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class CustomMusicHandler : MonoBehaviour,IActivateAbleUI
{
    public static CustomMusicHandler Instance;
    public Transform musicContainer;
    public MusicDropdown tmpDropdown;
    public TMP_Text mCountText;
    public List<CustomMusicContainer> customMusicContainers = new List<CustomMusicContainer>();
    private void Start()
    {
        Instance = this;
        for (int i = 0; i < musicContainer.childCount; i++)
        {
            customMusicContainers.Add(musicContainer.GetChild(i).GetComponent<CustomMusicContainer>());
        }
        updateUI();
    }
    public void onActivateEvent()
    {

    }
    public void addCustomMusic()
    {
        if(GameDataManager.Instance.getCustomMusicDatas().Count >= DatabaseManager.Instance.maxCustomMusic) { return; }
        GameDataManager.Instance.addCustomMusicData();
        updateUI();
    }
    public void updateUI()
    {
        foreach (CustomMusicContainer cm in customMusicContainers) cm.gameObject.SetActive(false);
        List<CustomMusicData> cmD = GameDataManager.Instance.getCustomMusicDatas();
        for (int i = 0; i < cmD.Count; i++)
        {
            customMusicContainers[i].gameObject.SetActive(true);
            customMusicContainers[i].updateData(cmD[i]);
        }
        mCountText.text = cmD.Count + "/" + DatabaseManager.Instance.maxCustomMusic;
    }
    public static bool ValidateValidMusicPath(string mPath)
    {
        print(mPath + "EXIST:" + System.IO.File.Exists(mPath));
        return System.IO.File.Exists(mPath);
    }
    public void updateDropdownChoices()
    {
        tmpDropdown.updateDropdownChoices();
    }
}
public interface IActivateAbleUI
{
    void onActivateEvent();
}