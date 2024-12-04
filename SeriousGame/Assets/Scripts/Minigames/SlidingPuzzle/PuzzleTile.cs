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
        if (gameObject.transform.localPosition.x + gameObject.GetComponent<BoxCollider2D>().size.x == puzzleScript.blankPiece.x || //if right side is touching blank's left
            gameObject.transform.localPosition.x == puzzleScript.blankPiece.x + gameObject.GetComponent<BoxCollider2D>().size.x || //if left side is touching blank's right
            gameObject.transform.localPosition.y + gameObject.GetComponent<BoxCollider2D>().size.y == puzzleScript.blankPiece.y || //if bottom is touching blank's top
            gameObject.transform.localPosition.y == puzzleScript.blankPiece.y + gameObject.GetComponent<BoxCollider2D>().size.y) //if top is touching blank's bottom
        {
            if (gameObject.transform.localPosition.x == puzzleScript.blankPiece.x || gameObject.transform.localPosition.y == puzzleScript.blankPiece.y && //if next to
                gameObject.GetComponent<SpriteRenderer>().enabled) //and not blank
            {
                canMove = true;
            }
            else
                canMove = false;
        }
        else
            canMove = false;

        //Testing
        if(canMove)
        {
            Debug.Log(gameObject.name);
        }
    }

    //Move tile
    public void MoveTile()
    {
        if (canMove)
        {
            Vector2 temp = gameObject.transform.localPosition;
            tweenPosition.SetPositionX(puzzleScript.blankPiece.x);
            tweenPosition.SetPositionY(puzzleScript.blankPiece.y);
            puzzleScript.blankPiece = temp;
        }
    }

}
