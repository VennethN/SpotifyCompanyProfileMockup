using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInstance : MonoBehaviour, IPoolAble
{
    public float thisNoteBeat;
    public int thisNoteID;
    public Transform noteStart;
    public Transform noteEnd;
    public TrailRenderer trailR;
    public string particleName;
    RhythmGameManager RGM;
    private void Start()
    {
        trailR = GetComponent < TrailRenderer>();
        trailR.emitting = true;
        RGM = RhythmGameManager.Instance;
    }
    private void Update()
    {
        if (noteStart == null || noteEnd == null) { return; }
        float relativeYPosToEnd = transform.position.y - noteEnd.position.y;
        float absYPosToEnd = Mathf.Abs(relativeYPosToEnd);
        if(absYPosToEnd < 0.6f && Input.GetKeyDown(RhythmGameManager.Instance.intToKeyCode[thisNoteID]))
        {
            noteHit();
            return;
        }
        if (relativeYPosToEnd < -2f)
        {
            noteMiss(); return;
        }
        if (relativeYPosToEnd > 0.1f)
        {
            transform.position = Vector2.Lerp(noteStart.position, noteEnd.position, (RGM.beatsShownInAdvance - (thisNoteBeat - RGM.songPlayedBeats)) / RGM.beatsShownInAdvance);
        }
        else
        {
            transform.position = transform.position - new Vector3(0,(RhythmGameManager.Instance.pastSpeed * Time.deltaTime));
        }
    }
    public void initNote(float noteBeat,Transform noteS, Transform noteE)
    {
        thisNoteBeat = noteBeat;
        noteStart = noteS;
        noteEnd = noteE;
        trailR.emitting = true;
    }
    public void noteHit()
    {
        RhythmGameManager.Instance.noteHit();
        GameVFXManager.SpawnParticle(particleName, transform.position);
        gameObject.SetActive(false);
    }
    public void noteMiss()
    {
        RhythmGameManager.Instance.noteMiss();
        gameObject.SetActive(false);
    }
    public void OnEnable()
    {
    }
    public void OnDisable()
    {
        trailR.emitting = false;
        trailR.Clear();
        ObjectPool.ReturnGameObject(this);
    }

}
