using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingCard : MonoBehaviour
{
    public bool flipped=false;
    public int cardType;

    SpriteRenderer _renderer;
    TweenScale _scaleFx;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _scaleFx = GetComponent<TweenScale>();
    }

    public void FlipCard()
    {
        if (flipped) return;
        FlipCardOver();
        MatchingMinigame.Inst.CheckCard(this);
    }
    public void FlipCardOver()
    {
        flipped = !flipped;
        _renderer.sprite = MatchingMinigame.Inst.cardSprites[(flipped) ? cardType : 0];
        _scaleFx.SetImmediateScaleX(0.2f);
    }
}
