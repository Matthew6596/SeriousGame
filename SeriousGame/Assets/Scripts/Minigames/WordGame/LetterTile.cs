using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class LetterTile : MonoBehaviour
{
    public LetterTileSlot attachedSlot = null, prevSlot=null;
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
        if(walls.Count!=0) tweenPos.SetPosition(transform);
        //if colliding with other tile, override tween position (push)
        else if (prevSlot!=null) tweenPos.SetPosition(attachedSlot.transform);
        else if (collisions.Count != 0) tweenPos.SetPosition(transform);
    }

    List<Collision2D> collisions = new();
    List<Collision2D> walls = new();
    public List<LetterTileSlot> slots = new();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Keep track of collisions with other tiles
        if (collision.gameObject.CompareTag("wall")) walls.Add(collision);
        else if (attachedSlot != null) return;
        else if (collision.gameObject.GetComponent<LetterTile>() != null) collisions.Add(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (walls.Contains(collision)) walls.Remove(collision);
        else if (attachedSlot != null) return;
        else if (collisions.Contains(collision)) collisions.Remove(collision);
    }
    public void SlotEnter(LetterTileSlot slot) { if(!slots.Contains(slot))slots.Add(slot); }
    public void SlotExit(LetterTileSlot slot) { if (slots.Contains(slot)) { slots.Remove(slot); }if(pickedup) slot.heldTile = null; }

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
        if (!pickedup) return;
        pickedup=false;
        //Get Closest slot
        float dist = float.MaxValue;
        LetterTileSlot closest=null;
        foreach(LetterTileSlot slot in slots)
        {
            if (slot.heldTile != null) continue;
            float d = Vector2.Distance(transform.position, slot.transform.position);
            if (d < dist)
            {
                dist = d;
                closest = slot;
            }
        }
        if (closest == null)
        {
            if (attachedSlot != null)
            {
                attachedSlot.DropTile(this);
                attachedSlot = null;
                tweenPos.SetPosition(transform);
            }
            prevSlot = null;
            return;
        }
        else if (closest.heldTile != null) return;

        //Attach to slot
        closest.SetTile(this);
    }
}
