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
        if (gameObject.transform.localPosition.x == puzzleScript.blankPiece.x || gameObject.transform.localPosition.y == puzzleScript.blankPiece.y)
        {
            canMove = true;
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
