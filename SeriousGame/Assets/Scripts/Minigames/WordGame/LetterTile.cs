using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterTile : MonoBehaviour
{
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Keep track of collisions with other tiles
        if (collision.gameObject.GetComponent<LetterTile>() != null) collisions.Add(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collisions.Contains(collision)) collisions.Remove(collision);
    }
}
