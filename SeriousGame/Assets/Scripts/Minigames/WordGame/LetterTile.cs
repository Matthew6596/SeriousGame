using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterTile : MonoBehaviour
{
    LetterTileSlot attachedSlot = null;
    TweenPosition tweenPos;
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
        if (collisions.Count != 0) tweenPos.SetPosition(transform);
    }

    List<Collision2D> collisions = new();
    List<Collision2D> slots = new();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Keep track of collisions with other tiles
        if (collision.gameObject.GetComponent<LetterTile>() != null) collisions.Add(collision);
        else if (collision.gameObject.GetComponent<LetterTileSlot>() != null)
        {
            slots.Add(collision);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collisions.Contains(collision)) collisions.Remove(collision);
        else if (slots.Contains(collision))
        {
            slots.Remove(collision);
        }
    }

    public void Released()
    {
        //Get Closest slot
        float dist = float.MaxValue;
        LetterTileSlot closest=null;
        foreach(Collision2D c in slots)
        {
            LetterTileSlot slot = c.gameObject.GetComponent<LetterTileSlot>();
            float d = Vector2.Distance(transform.position, slot.transform.position);
            if (d < dist)
            {
                dist = d;
                closest = slot;
            }
        }
        if (closest == null) {
            if (attachedSlot != null)
            {
                attachedSlot.DropTile(this);
                attachedSlot = null;
            }
            return;
        }

        //Attach to slot
        attachedSlot = closest;
        closest.SetTile(this);
    }
}
