using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Diagnostics;

public class LetterTile : MonoBehaviour
{
    public LetterTileSlot attachedSlot = null;
    public TweenPosition tweenPos;
    public bool pickedup = false;
    public AudioClip tileDropSfx;
    private char _letter;

    private AudioSource src;
    public char letter { get => _letter; set
        {
            //When letter is set, update the text label too
            if(letterLabel==null) letterLabel = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            letterLabel.text = value.ToString();
            _letter = value;
        }
    }
    Vector2 initPos;
    bool attachable = false;

    Stopwatch mouseDownTime=new();
    Vector2 downPos;

    TMP_Text letterLabel;
    // Start is called before the first frame update
    void Start()
    {
        tweenPos = GetComponent<TweenPosition>();
        src = GetComponent<AudioSource>();
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if colliding with other tile, override tween position (push)
        if(attachedSlot!=null) tweenPos.SetPosition(attachedSlot.transform);
        else if (walls.Count!=0 || collisions.Count!=0) tweenPos.SetPosition(transform);
    }

    List<Collision2D> collisions = new();
    List<Collision2D> walls = new();
    public List<LetterTileSlot> slots = new();
    public void ClearCollisions() { collisions.Clear(); }
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
    public void SlotEnter(LetterTileSlot slot) 
    {
        if (!slots.Contains(slot))
        {
            slots.Add(slot);
            if(attachable)slot.SetTile(this);
        }
    }
    public void SlotExit(LetterTileSlot slot) 
    { 
        if (slots.Contains(slot)) slots.Remove(slot);
        //if(pickedup) slot.heldTile = null; 
    }

    bool upFromSlot = false;
    public void Pickup()
    {
        if (attachedSlot != null) { attachedSlot.DropTile(this); upFromSlot = true; }
        pickedup = true;
        mouseDownTime = Stopwatch.StartNew();
        downPos = transform.position;
    }
    public void Released()
    {
        if (!pickedup) return;
        src.PlayOneShot(tileDropSfx);
        pickedup = false;

        //UnityEngine.Debug.Log("click time: " + mouseDownTime.ElapsedMilliseconds+"ms");
        if (mouseDownTime.ElapsedMilliseconds < 100 || (mouseDownTime.ElapsedMilliseconds<600&&Vector2.Distance(downPos,transform.position)<0.5f))
        {
            if (!upFromSlot) WordChecker.SetNextSlotTile(this);
            else { 
                tweenPos.SetPositionX(initPos.x); tweenPos.SetPositionY(initPos.y);
                MenuManager.DelayAction(.5f, () => { tweenPos.SetPosition(transform); enabled = true; });
                enabled = false;
            }
            upFromSlot = false;
            
            return;
        }
        upFromSlot = false;

        attachable = true;
        MenuManager.DelayAction(.2f, () => { attachable = false; });

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
                tweenPos.SetPosition(transform);
            }
            return;
        }
        else if (closest.heldTile != null) return;

        //Attach to slot
        closest.SetTile(this);
    }
}
