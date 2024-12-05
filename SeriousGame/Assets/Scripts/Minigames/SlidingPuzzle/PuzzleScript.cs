using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PuzzleScript : MonoBehaviour
{
    public bool canWin = false;
    public int numOfPieces;
    public GameObject[] puzzlePieces;
    public Vector2[] positions;

    public Vector2 blankPiece;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        MenuManager.lastMinigame = "SlidingPuzzle";
        MenuManager.DelayAction(0.6f, () => { ChooseBlankPiece(); });
        MenuManager.DelayAction(0.6f, () => { RandomizePositions(); });
        //canWin = true; //Disabled for testing
    }

    // Update is called once per frame
    void Update()
    {
        //Update blank piece's position
        puzzlePieces[index].transform.localPosition = blankPiece;

        if (CheckPuzzlePositions() && canWin)
        {
            MenuManager.DelayAction(0.6f, () => { MenuManager.Inst.ChangeScene("WinScreen"); });
            Debug.Log("You Win!");
        }
    }

    bool CheckPuzzlePositions()
    {
        bool correct = true;

        for(int i = 0; i < numOfPieces; i++)
        {
            if ((Vector2)puzzlePieces[i].transform.localPosition != positions[i])
                correct = false;

            //Debug.Log(puzzlePieces[i].transform.localPosition);
            //Debug.Log(correct);
        }

        return correct;
    }

    void ChooseBlankPiece()
    {
        //index = Random.Range(0, numOfPieces);
        index = Random.Range(1, numOfPieces - 1);
        blankPiece = puzzlePieces[index].transform.localPosition;
        puzzlePieces[index].GetComponent<SpriteRenderer>().enabled = false;
        puzzlePieces[index].GetComponent<MouseInteractable>().enabled = false;
    }

    void RandomizePositions()
    {
        //I think TweenPosition is messing with this????

        Vector2 temp = puzzlePieces[0].transform.localPosition; //store first piece's pos
        puzzlePieces[0].GetComponent<TweenPosition>().SetPositionX(puzzlePieces[numOfPieces - 1].transform.localPosition.x);
        puzzlePieces[0].GetComponent<TweenPosition>().SetPositionY(puzzlePieces[numOfPieces - 1].transform.localPosition.y);
        puzzlePieces[0].transform.localPosition = puzzlePieces[numOfPieces - 1].transform.localPosition;

        puzzlePieces[numOfPieces - 1].GetComponent<TweenPosition>().SetPositionX(temp.x);
        puzzlePieces[numOfPieces - 1].GetComponent<TweenPosition>().SetPositionY(temp.y);
        puzzlePieces[numOfPieces - 1].transform.localPosition = temp;

        //This is bad and doesn't work
        //int i = Random.Range(1, numOfPieces - 2);

        //for (int ctr = 0; ctr < numOfPieces - 1; ctr++)
        //{
        //    GameObject puzzlePiece = puzzlePieces[i];
        //    if (puzzlePiece.GetComponent<SpriteRenderer>().enabled) //if not blank
        //    {
        //        if (ctr == 0)
        //            puzzlePiece.transform.localPosition = puzzlePieces[numOfPieces - 1].transform.localPosition; //give first piece the last piece's pos
        //        if (ctr == numOfPieces - 1)
        //            puzzlePiece.transform.localPosition = temp; //give last piece the first piece's pos
        //        else if (ctr != 0)
        //        {
        //            puzzlePiece.transform.localPosition = puzzlePieces[i - 1].transform.localPosition;
        //        }

        //        if (i + 1 < numOfPieces - 1)
        //        {
        //            i++;
        //        }
        //        else
        //            i = 0;

        //    }
        //    puzzlePieces[i] = puzzlePiece;
        //}

    }

}
