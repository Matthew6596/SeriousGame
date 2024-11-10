using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordChecker : MonoBehaviour
{
    enum WordState { None, Valid, Invalid}
    WordState wordState = WordState.None;

    public string word="";

    public TextAsset[] wordDictionaries;
    public LetterTileSlot[] tileSlots;

    private Vector3 targetColor=new(.5f,.5f,.5f);
    private SpriteRenderer _sprRender;

    public List<string> playedWords = new();
    string errorMsg;

    // Start is called before the first frame update
    void Start()
    {
        _sprRender = GetComponent<SpriteRenderer>();
        for( int i=0; i<tileSlots.Length; i++)
        {
            tileSlots[i].onLetterEntered += (s, e) => { 
                CheckForWord();
                if (e.nextSlot!=null)
                {
                    e.nextSlot.gameObject.SetActive(true);
                }
            };
            tileSlots[i].onLetterExited += (s, e) => { 
                CheckForWord();
                checkSlotsShown();
            };
            if(i+1<tileSlots.Length) tileSlots[i].nextSlot = tileSlots[i+1];
            if(i!=0)tileSlots[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 col = Tween.LazyTweenScaled(new(_sprRender.color.r, _sprRender.color.g, _sprRender.color.b), targetColor, 0.1f);
        _sprRender.color = new Color(col.x,col.y,col.z);
    }

    private void checkSlotsShown()
    {
        for(int i=tileSlots.Length-1; i>0; i--)
        {
            if (tileSlots[i - 1].heldTile == null && tileSlots[i].heldTile == null) tileSlots[i].gameObject.SetActive(false);
            else break;
        }
    }
    public void CheckForWord()
    {
        word = "";
        bool gapCheck = false;
        for (int i = 0; i < tileSlots.Length; i++)
        {
            if (tileSlots[i].heldTile == null)
            {
                gapCheck = true;
                continue;
            }
            else if (gapCheck)
            {
                IncompleteWord();
                return;
            }
            word += tileSlots[i].heldTile.letter;
        }
        Debug.Log(word);
        CheckDictionary();
    }

    private void CheckDictionary()
    {
        //Open dictionary based on word length (dictionaries should be sorted alphabetically)
        int lengthIndex = word.Length - 3; //start with 3 letter words
        if (lengthIndex < 0)
        {
            IncompleteWord();
            return;
        }
        TextAsset dictionary = wordDictionaries[lengthIndex];
        List<string> words = new(); words.AddRange(dictionary.text.Split('\n', '\r'));

        if (words.Contains(word.ToLower()))
        {
            if (playedWords.Contains(word)) InvalidWord("Word already played");
            else ValidWord();
        }
        else InvalidWord("Word not in word list");
    }

    private void IncompleteWord()
    {
        errorMsg = "Word must be at least 3 letters long";
        targetColor = new(.5f, .5f, .5f);
        wordState = WordState.None;
    }
    private void InvalidWord(string reason)
    {
        targetColor = new(1, 0, 0);
        wordState = WordState.Invalid;
        errorMsg = reason;
    }
    private void ValidWord()
    {
        targetColor = new(0, 1, 0);
        wordState = WordState.Valid;
    }

    public int score = 0;
    public void SubmitWord()
    {
        switch (wordState)
        {
            case WordState.None:
                //Give invalid message
                break;
            case WordState.Invalid:
                //Give "not a word" message
                DropAllTiles();
                break;
            case WordState.Valid:
                playedWords.Add(word);
                score += (word.Length) switch { 3=>3,4=>4,5=>6,7=>10,8=>12,_=>3};
                DropAllTiles();
                break;
        }
        Debug.Log(wordState+": "+score);
    }
    private void DropAllTiles()
    {
        for (int i = 0; i < tileSlots.Length; i++)
        {
            if (tileSlots[i].heldTile != null)
            {
                tileSlots[i].heldTile.gameObject.GetComponent<TweenPosition>().MovePositionY(-1.8f);
                tileSlots[i].DropTile(tileSlots[i].heldTile);
            }
            else break;
        }
    }
}
