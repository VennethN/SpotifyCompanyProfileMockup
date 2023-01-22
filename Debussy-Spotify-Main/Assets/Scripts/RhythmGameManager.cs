using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class RhythmGameManager : MonoBehaviour
{
    public static RhythmGameManager Instance;
    public AudioSource audioSource;
    public SongRhythmData currentSongData;
    public double songStartTime;
    public float songPlayedTime;
    public float songPlayedBeats;
    public float secondsPerBeats;
    public Transform startPos;
    public List<Transform> arrowStartPos;
    public List<Transform> arrowEndPos;
    public List<NoteInstance> notesPrefab;
    public List<int> currentIndexes;
    public Dictionary<int, KeyCode> intToKeyCode;
    public int scorePerHit;
    public int currentStreak;
    public int totalScore;
    public GameObject streakObject;
    public float beatsShownInAdvance;
    public Animator scoreAnimator;
    public Animator streakAnimator;
    public Animator cdAnimator;
    public SpriteRenderer songCover;
    public TMP_Text songTitle;
    public TMP_Text scoreText;
    public TMP_Text songTimer;
    public TMP_Text streakText;
    private bool stopped;
    public float pastSpeed;
    private void Awake()
    { 

        stopped = false;
        Instance = this;
    }
    private void Start()
    {
        currentSongData = DatabaseManager.Instance.currentSelectedSong;
        beatsShownInAdvance = currentSongData.beatsShownInAdvance;
        intToKeyCode = new Dictionary<int, KeyCode>();
        intToKeyCode[0] = KeyCode.LeftArrow;
        intToKeyCode[1] = KeyCode.DownArrow;
        intToKeyCode[2] = KeyCode.UpArrow;
        intToKeyCode[3] = KeyCode.RightArrow;
        secondsPerBeats = 60f / currentSongData.songBPM;
        songStartTime = AudioSettings.dspTime;
        audioSource.clip = currentSongData.songAudio;
        songCover.sprite = currentSongData.songCover;
        songTitle.text = currentSongData.artist == "" ? currentSongData.songName : currentSongData.artist + " - " + currentSongData.songName;
        audioSource.Play();
        pastSpeed = currentSongData.songBPM / 30f * 5f/beatsShownInAdvance;
    }
    public void noteHit()
    {
        currentStreak++;
        totalScore += scorePerHit * currentStreak / 10;
        streakObject.SetActive(true);
        streakAnimator.SetTrigger("scoreAdded");
        scoreAnimator.SetTrigger("scoreAdded");
        scoreText.text = totalScore.ToString();
        streakText.text = currentStreak.ToString() + "x";
    }
    public void noteMiss()
    {
        currentStreak = 0;
        streakObject.SetActive(false);
    }
    private void Update()
    {
        songPlayedTime = (float)(AudioSettings.dspTime - songStartTime);
        songPlayedBeats = songPlayedTime / secondsPerBeats;
        int minute = Mathf.FloorToInt(songPlayedTime / 60);
        int sekon = Mathf.FloorToInt(songPlayedTime % 60);
        songTimer.text = (minute >= 10 ? minute.ToString() : "0" + minute.ToString()) + ":" + (sekon >= 10 ? sekon.ToString() : "0" + sekon.ToString());
        for (int i =0;i<currentIndexes.Count;i++)
        {
            if(currentIndexes[i] < currentSongData.idData[i].notesData.Count && currentSongData.idData[i].notesData[currentIndexes[i]] < songPlayedBeats + beatsShownInAdvance)
            {
                NoteInstance spawnedNote = ObjectPool.GetObject<NoteInstance>(notesPrefab[i], arrowStartPos[i].position, notesPrefab[i].transform.rotation);
                spawnedNote.initNote(currentSongData.idData[i].notesData[currentIndexes[i]], arrowStartPos[i], arrowEndPos[i]);
                currentIndexes[i]++;
            }
        }
        if(songPlayedTime > audioSource.clip.length && stopped == false)
        {
            stopped = true;
            cdAnimator.enabled = false;
            GameDataManager.Instance.tryUpdateSongHighscoreData(currentSongData.songID, totalScore);
            TransitionHandler.Instance.initTransition("BasicFade", transferBackScene);
        }
    }
    public void transferBackScene()
    {
        SceneManager.UnloadSceneAsync(2);
        updateMainM();
        AudioManager.ResumeMusic();
    }
    public void updateMainM()
    {
        MainMenuManager.Instance.eventSystem.SetActive(true);
        MainMenuManager.Instance.audioListener.enabled = true;
        MainMenuManager.Instance.upRhythmMenu();
    }

}
