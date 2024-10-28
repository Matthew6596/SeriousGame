using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingCard : MonoBehaviour
{
    public bool flipped=false;
    public int cardType;

    SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void FlipCard()
    {
        FlipCardOver();
        MatchingMinigame.Inst.CheckCard(this);
    }
    public void FlipCardOver()
    {
        flipped = !flipped;
        _renderer.sprite = MatchingMinigame.Inst.cardSprites[(flipped) ? cardType : 0];
    }
}
