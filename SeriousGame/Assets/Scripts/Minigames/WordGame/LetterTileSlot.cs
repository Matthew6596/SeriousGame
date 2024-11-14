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
    UnityAction setTilePosAction;
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if is letter tile
        if (collision.GetComponent<LetterTile>() == null || heldTile!=null) return;

        //Get draggable script from tile
        MouseDraggable lmd = collision.GetComponent<MouseDraggable>();
        //Create action to set the tiles position to this slot
        setTilePosAction = () => { lmd.GetComponent<TweenPosition>().SetPosition(transform); };
        //If still dragging, wait and add listener for when dragging is done
        if (lmd.dragging)
        {
            lmd.onMouseCancel.AddListener(setTilePosAction);
            lmd.onMouseUp.AddListener(setTilePosAction);
        }
        else //not dragging, just set the tile's position
        {
            setTilePosAction();
        }
        heldTile = lmd.GetComponent<LetterTile>(); //set the tile as being in this slot
        onLetterEntered?.Invoke(null,this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DropTile(collision.GetComponent<LetterTile>());
    }*/
    public void DropTile(LetterTile tile)
    {
        if (tile == null || heldTile != tile) return;

        //Remove listeners for setting the tile's position here
        //MouseDraggable lmd = heldTile.GetComponent<MouseDraggable>();
        //lmd.onMouseCancel.RemoveListener(setTilePosAction);
        //lmd.onMouseUp.RemoveListener(setTilePosAction);
        //No more tile here
        heldTile = null;

        onLetterExited?.Invoke(null, this);
    }
    public void SetTile(LetterTile tile)
    {
        heldTile = tile;
        onLetterEntered?.Invoke(null, this);
        heldTile.gameObject.GetComponent<TweenPosition>().SetPosition(transform);
    }
}
