using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public GameData currentData;

    private void Awake()
    {//making sure there's only one Instance  of the gamecore class in a game
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadData();
    }
    public int getSongHighscoreData(int songID)
    {
        for(int i =0;i<currentData.songHighscores.Count;i++)
        {
            if (songID != currentData.songHighscores[i].songID) continue;
            return currentData.songHighscores[i].songHighscore;
        }
        throw new Exception("EC 1 Song ID doesnt match");
    }
    public List<CustomMusicData> getCustomMusicDatas()
    {
        return currentData.customMusicDatas;
    }
    public void updateCustomMusicTitle(int cIndex,string nTitle)
    {
        List<CustomMusicData> lcm =  getCustomMusicDatas();
        lcm[cIndex].customMusicTitle = nTitle;
        Save();
    }
    public void updateCustomMusicPath(int cIndex, string nPath)
    {
        List<CustomMusicData> lcm = getCustomMusicDatas();
        lcm[cIndex].customMusicPath = nPath;
        Save();
    }
    public void addCustomMusicData()
    {
        getCustomMusicDatas().Add(new CustomMusicData());
        Save();
    }
    public void removeCustomMusicData(int ind)
    {
        getCustomMusicDatas().RemoveAt(ind);
        Save();
    }
    public void updateSongHighscoreData(int songID,int newHighscore)
    {
        for (int i = 0; i < currentData.songHighscores.Count; i++)
        {
            if (songID != currentData.songHighscores[i].songID) continue;
            currentData.songHighscores[i].songHighscore = newHighscore;
            Save();
            return;
        }
        throw new Exception("EC 2 Song ID doesnt match");
    }

    public bool tryUpdateSongHighscoreData(int songID,int newHighscore)
    {
        for (int i = 0; i < currentData.songHighscores.Count; i++)
        {
            if (songID != currentData.songHighscores[i].songID) continue;
            if(currentData.songHighscores[i].songHighscore >= newHighscore) { return false; }
            currentData.songHighscores[i].songHighscore = newHighscore;
            Save();
            return true;
        }
        throw new Exception("EC 3 Song ID doesnt match");
    }
    public void Save()//Method to save player's data
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SpotifyCompanyProfileData.dat");
        PlayerData data = new PlayerData();
        data.data = currentData;
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadData()//Method to load player's data
    {
        if (File.Exists(Application.persistentDataPath + "/SpotifyCompanyProfileData.dat"))
        {
            Debug.Log("DATA FOUND");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SpotifyCompanyProfileData.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            currentData = data.data;
            file.Close();
        }
        else
        {
            currentData = new GameData();
            currentData.songHighscores = new List<SongHighscoreData>();
            currentData.customMusicDatas = new List<CustomMusicData>();
        }
        checkHighscoreDatas();
        // Save();

    }
    public void checkHighscoreDatas()

    {
        List<SongHighscoreData> oldData = currentData.songHighscores;
        List<SongHighscoreData> newData = new List<SongHighscoreData>();
        for(int i =0;i<DatabaseManager.Instance.RhythmSongs.Count;i++)
        {
            newData.Add(new SongHighscoreData(DatabaseManager.Instance.RhythmSongs[i].songID,0));
        }
        foreach(SongHighscoreData SHD in oldData)
        {
            for(int i =0;i<newData.Count;i++)
            {
                if(newData[i].songID == SHD.songID)
                {
                    newData[i].songHighscore = SHD.songHighscore;
                }
            }
        }
        currentData.songHighscores = newData;
    }
}
[System.Serializable]
public class GameData
{
    public List<SongHighscoreData> songHighscores;
    public List<CustomMusicData> customMusicDatas;

}
[System.Serializable]
public class SongHighscoreData
{
    public int songID;
    public int songHighscore;
    public SongHighscoreData(int songIDS,int songHS)
    {
        songID = songIDS;
        songHighscore = songHS;
    }
}
[System.Serializable]
public class CustomMusicData
{
    public string customMusicTitle;
    public string customMusicPath;
    public CustomMusicData()
    {

    }
}

[System.Serializable]
public class PlayerData
{
    public GameData data;
}
