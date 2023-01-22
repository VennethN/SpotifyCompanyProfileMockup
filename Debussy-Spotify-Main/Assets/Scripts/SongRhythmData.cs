using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "SongRhythmData", menuName = "ScriptableObjects/SongRhythmData", order = 1)]
public class SongRhythmData : ScriptableObject
{
    public float songBPM;
    public int songID;
    public string songName;
    public string artist;
    public int beatsShownInAdvance;
    public Sprite songCover;
    public AudioClip songAudio;
    public List<noteIDData> idData;

}

[System.Serializable]
public class noteIDData
{
    public List<float> notesData;
}
