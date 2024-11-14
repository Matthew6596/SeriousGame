using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class LetterTile : MonoBehaviour
{
    LetterTileSlot attachedSlot = null;
    public TweenPosition tweenPos;
    public bool pickedup = false;
    private char _letter;
    public char letter { get => _letter; set
        {
            //When letter is set, update the text label too
            if(letterLabel==null) letterLabel = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            letterLabel.text = value.ToString();
            _letter = value;
        }
    }

    TMP_Text letterLabel;
    // Start is called before the first frame update
    void Start()
    {
        tweenPos = GetComponent<TweenPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        //if colliding with other tile, override tween position (push)
        if (attachedSlot != null) tweenPos.SetPosition(attachedSlot.transform);
        else if (collisions.Count != 0) tweenPos.SetPosition(transform);
    }

    List<Collision2D> collisions = new();
    public List<LetterTileSlot> slots = new();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Keep track of collisions with other tiles
        if (attachedSlot != null) return;
        if (collision.gameObject.GetComponent<LetterTile>() != null) collisions.Add(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (attachedSlot != null) return;
        if (collisions.Contains(collision)) collisions.Remove(collision);
    }
    public void SlotEnter(LetterTileSlot slot) { if(!slots.Contains(slot)&&slot.heldTile==null)slots.Add(slot); }
    public void SlotExit(LetterTileSlot slot) { if(slots.Contains(slot))slots.Remove(slot); }

    public void Pickup()
    {
        if (attachedSlot != null)
        {
            attachedSlot.DropTile(this);
            //attachedSlot = null;
        }
        pickedup = true;
    }
    public void Released()
    {
        pickedup=false;
        //Get Closest slot
        float dist = float.MaxValue;
        LetterTileSlot closest=null;
        foreach(LetterTileSlot slot in slots)
        {
            float d = Vector2.Distance(transform.position, slot.transform.position);
            if (d < dist)
            {
                dist = d;
                closest = slot;
            }
        }
        if (closest == null) {
            if (slots.Count > 0)
            {
                attachedSlot = slots[0];
                attachedSlot.SetTile(this);
            }
            else if (attachedSlot != null)
            {
                attachedSlot.DropTile(this);
                attachedSlot = null;
                tweenPos.SetPosition(transform);
            }
            return;
        }

        //Attach to slot
        attachedSlot = closest;
        attachedSlot.SetTile(this);
    }
}
