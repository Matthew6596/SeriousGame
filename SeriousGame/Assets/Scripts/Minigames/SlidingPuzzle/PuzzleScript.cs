using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PuzzleScript : MonoBehaviour
{
    public int numOfPieces;
    public GameObject[] puzzlePieces;
    public Vector2[] positions;

    // Start is called before the first frame update
    void Start()
    {
        MenuManager.lastMinigame = "SlidingPuzzle";
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckPuzzlePositions())
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

}
