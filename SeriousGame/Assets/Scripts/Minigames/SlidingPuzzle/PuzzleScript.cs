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
        MenuManager.DelayAction(0.8f, () => { RandomizePositions(); });
        MenuManager.DelayAction(1f, () => { canWin = true; }); //Disabled for testing
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
        bool correct = false;

        for (int i = 0; i < numOfPieces; i++)
        {

            //if below doesn't work, use TweenPosition.targetPos

            //Margin of error for x
            if (puzzlePieces[i].transform.localPosition.x <= positions[i].x + 2
                && puzzlePieces[i].transform.localPosition.x >= positions[i].x - 2)
            {
                //Margin of error for y
                if (puzzlePieces[i].transform.localPosition.y <= positions[i].y + 2
                    && puzzlePieces[i].transform.localPosition.y >= positions[i].y - 2)
                    correct = true;
            }
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
        Vector2 temp = puzzlePieces[0].transform.localPosition; //store first piece's pos

        //give first piece the last piece's pos
        puzzlePieces[0].GetComponent<TweenPosition>().SetPositionX(puzzlePieces[numOfPieces - 1].transform.localPosition.x);
        puzzlePieces[0].GetComponent<TweenPosition>().SetPositionY(puzzlePieces[numOfPieces - 1].transform.localPosition.y);
        puzzlePieces[0].transform.localPosition = puzzlePieces[numOfPieces - 1].transform.localPosition;

        //give last piece the first piece's pos
        puzzlePieces[numOfPieces - 1].GetComponent<TweenPosition>().SetPositionX(temp.x);
        puzzlePieces[numOfPieces - 1].GetComponent<TweenPosition>().SetPositionY(temp.y);
        puzzlePieces[numOfPieces - 1].transform.localPosition = temp;

        int i = 1;
        while(i % 2 != 0)
        {
            i = Random.Range(2, numOfPieces - 3);
        }

        for (int ctr = 0; ctr < numOfPieces - 1; ctr++)
        {
            GameObject puzzlePiece = puzzlePieces[i];
            if (puzzlePiece.GetComponent<SpriteRenderer>().enabled && puzzlePieces[i - 1].GetComponent<SpriteRenderer>().enabled) //if not blank & previous not blank
            {
                if (i != 0 && i != numOfPieces - 1)
                {
                    //Store pos
                    temp = puzzlePiece.transform.localPosition;

                    //Swap
                    puzzlePiece.GetComponent<TweenPosition>().SetPositionX(puzzlePieces[i - 1].transform.localPosition.x);
                    puzzlePiece.GetComponent<TweenPosition>().SetPositionY(puzzlePieces[i - 1].transform.localPosition.y);
                    puzzlePiece.transform.localPosition = puzzlePieces[i - 1].transform.localPosition;

                    puzzlePieces[i-1].GetComponent<TweenPosition>().SetPositionX(temp.x);
                    puzzlePieces[i-1].GetComponent<TweenPosition>().SetPositionY(temp.y);
                    puzzlePiece.transform.localPosition = temp;
                }
                
                puzzlePieces[i] = puzzlePiece;

                if (i + 2 < numOfPieces - 1)
                {
                    //i++;
                    i += 2;
                }
                else
                    i = 2;
            }
        }
    }

}
