using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RhythmMenuHandler : MonoBehaviour
{
    public int navigationIndex;
    public Image songImage;
    public Image songBackground;
    public TMP_Text artistText;
    public TMP_Text songTitle;
    public TMP_Text highscoreText;
    public TMP_Text songLength;
    public Animator rhythmMenuAnimator;
    private bool allowClick = true;
    private void Start()
    {
        updateCurrentSelection();
    }
    public void updateCurrentSelection()
    {
        SongRhythmData currentD = DatabaseManager.Instance.RhythmSongs[navigationIndex];
        DatabaseManager.Instance.currentSelectedSong = currentD;
        songImage.sprite = currentD.songCover;
        songBackground.sprite = currentD.songCover;
        artistText.text = currentD.artist;
        songTitle.text = currentD.songName;
        float Aleng = currentD.songAudio.length;
        int minute = Mathf.FloorToInt(Aleng / 60);
        int sekon = Mathf.RoundToInt(Aleng % 60);
        songLength.text = (minute >= 10 ? minute.ToString() : "0" + minute.ToString()) + ":" + (sekon >= 10 ? sekon.ToString() : "0" + sekon.ToString());
        highscoreText.text = "Highscore: " + GameDataManager.Instance.getSongHighscoreData(currentD.songID).ToString();
    }
    public void transFinished()
    {
        allowClick = true;
    }
    public void navigateRight()
    {
        if (!allowClick) { return; }
        allowClick = false;
        navigationIndex++;
        navigationIndex %= DatabaseManager.Instance.RhythmSongs.Count;
        rhythmMenuAnimator.SetTrigger("navRight");
    }
    public void navigateLeft()
    {
        if (!allowClick) { return; }
        allowClick = false;
        navigationIndex--;
        if(navigationIndex < 0) { navigationIndex = DatabaseManager.Instance.RhythmSongs.Count - 1; }
        rhythmMenuAnimator.SetTrigger("navLeft");
    }
}
