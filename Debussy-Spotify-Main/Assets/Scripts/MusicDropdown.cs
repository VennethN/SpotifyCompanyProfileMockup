using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System.IO;
using UnityEditor;
using SFB;
public class MusicDropdown : MonoBehaviour
{
    public TMP_Dropdown tmpDropdown;
    public List<int> trueCustomIndexes;
    private void Start()
    {
        updateDropdownChoices();
    }
    public void onDropChanged()
    {
        int tmpIndex = tmpDropdown.value;
        if(tmpIndex < AudioManager.Instance.BGMAudio.Count)
        {
            AudioManager.ChangeMusicAudio(AudioManager.Instance.BGMAudio[tmpIndex].clip);
        }
        else
        {
            StartCoroutine(changeAud(GameDataManager.Instance.getCustomMusicDatas()[trueCustomIndexes[tmpIndex-AudioManager.Instance.BGMAudio.Count]].customMusicPath));
        }
    }
    IEnumerator changeAud(string pth)
    {
        CoroutineWithData cd = new CoroutineWithData(this, AudioManager.LoadAudioFromPath(pth));
        yield return cd.coroutine;
        AudioManager.ChangeMusicAudio((AudioClip)cd.result);
    }
    public void updateDropdownChoices()
    {
        tmpDropdown.ClearOptions();
        List<string> stringKeys = new List<string>();
        for (int i = 0; i < AudioManager.Instance.BGMAudio.Count; i++)
        {
            stringKeys.Add(AudioManager.Instance.BGMAudio[i].audioKey);
        }
        tmpDropdown.AddOptions(stringKeys);
        List<string> stringKeys2 = new List<string>();
        List<CustomMusicData> LCMData = GameDataManager.Instance.getCustomMusicDatas();
        trueCustomIndexes = new List<int>();
        for (int i = 0; i < LCMData.Count; i++)
        {
            if (!CustomMusicHandler.ValidateValidMusicPath(LCMData[i].customMusicPath)) { continue; }
            stringKeys2.Add(LCMData[i].customMusicTitle);
            trueCustomIndexes.Add(i);
        }
        tmpDropdown.AddOptions(stringKeys2);
    }
}
public class CoroutineWithData
{
    public Coroutine coroutine { get; private set; }
    public object result;
    private IEnumerator target;
    public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
    {
        this.target = target;
        this.coroutine = owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
    }
}