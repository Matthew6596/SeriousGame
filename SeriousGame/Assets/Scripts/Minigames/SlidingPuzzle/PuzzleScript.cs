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

    // Start is called before the first frame update
    void Start()
    {
        MenuManager.lastMinigame = "SlidingPuzzle";
        ChooseBlankPiece();
        //canWin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckPuzzlePositions() && canWin)
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
        int index = Random.Range(0, numOfPieces);
        blankPiece = puzzlePieces[index].transform.localPosition;
        puzzlePieces[index].GetComponent<SpriteRenderer>().enabled = false;
    }

}
