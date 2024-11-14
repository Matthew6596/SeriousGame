using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LetterTileSlot : MonoBehaviour
{
    public event EventHandler<LetterTileSlot> onLetterEntered,onLetterExited;
    public LetterTile heldTile;
    public LetterTileSlot nextSlot;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LetterTile tile = collision.GetComponent<LetterTile>();
        if (tile != null)
        {
            tile.SlotEnter(this);
            if (!tile.pickedup) SetTile(tile);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        LetterTile tile = collision.GetComponent<LetterTile>();
        if (tile != null)
        {
            tile.SlotExit(this);
        }
    }
    public void DropTile(LetterTile tile)
    {
        if (tile == null || heldTile != tile) return;

        heldTile = null;
        onLetterExited?.Invoke(null, this);
    }
    public void SetTile(LetterTile tile)
    {
        heldTile = tile;
        onLetterEntered?.Invoke(null, this);
        heldTile.tweenPos.SetPosition(transform);
    }
}
