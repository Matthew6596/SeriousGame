using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    PuzzleScript puzzleScript;
    bool canMove;
    TweenPosition tweenPosition;

    // Start is called before the first frame update
    void Start()
    {
        puzzleScript = GameObject.Find("PuzzleTiles").GetComponent<PuzzleScript>();
        //blankPiece = puzzleScript.blankPiece;

        tweenPosition = gameObject.GetComponent<TweenPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        //Works, but takes a few clicks to move the movable tiles? Their canMove is true even when clicking on them does nothing
        if (Mathf.Abs(gameObject.transform.localPosition.x + gameObject.GetComponent<BoxCollider2D>().size.x - puzzleScript.blankPiece.x)<.1f || //if right side is touching blank's left
            Mathf.Abs(gameObject.transform.localPosition.x - (puzzleScript.blankPiece.x + gameObject.GetComponent<BoxCollider2D>().size.x))<.1f || //if left side is touching blank's right
            Mathf.Abs(gameObject.transform.localPosition.y + gameObject.GetComponent<BoxCollider2D>().size.y - puzzleScript.blankPiece.y)<.1f || //if bottom is touching blank's top
            Mathf.Abs(gameObject.transform.localPosition.y - (puzzleScript.blankPiece.y + gameObject.GetComponent<BoxCollider2D>().size.y))<.1f) //if top is touching blank's bottom
        {
            if (Mathf.Abs(gameObject.transform.localPosition.x-puzzleScript.blankPiece.x)<0.1f || Mathf.Abs(gameObject.transform.localPosition.y-puzzleScript.blankPiece.y)<0.1f && //if next to
                gameObject.GetComponent<SpriteRenderer>().enabled) //and not blank
            {
                canMove = true;
            }
            else canMove = false;
        }
        else
            canMove = false;

        //Testing
        if(canMove)
        {
            //Debug.Log(gameObject.name);
        }
    }

    //Move tile
    public void MoveTile()
    {
        Debug.Log("clicked: canmove="+canMove);
        if (canMove)
        {
            Vector2 temp = gameObject.transform.localPosition;
            tweenPosition.SetPositionX(puzzleScript.blankPiece.x);
            tweenPosition.SetPositionY(puzzleScript.blankPiece.y);
            puzzleScript.blankPiece = temp;
        }
    }

}
