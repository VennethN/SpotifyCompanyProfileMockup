using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class ButtonController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite pressedImage;
    public Sprite defaultImage;
    public KeyCode pressKey;
    private int nextFrameCheckScore;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(nextFrameCheckScore > -1)
        {
            if(RhythmGameManager.Instance.totalScore == nextFrameCheckScore)
            {
                RhythmGameManager.Instance.noteMiss();
                nextFrameCheckScore = -99;
            }
        }
        if(Input.GetKeyDown(pressKey))
        {
            spriteRenderer.sprite = pressedImage;
            nextFrameCheckScore = RhythmGameManager.Instance.totalScore;

        }
        if(Input.GetKeyUp(pressKey))
        {
            spriteRenderer.sprite = defaultImage;
        }
    }
}
