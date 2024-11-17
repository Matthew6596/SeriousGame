using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WordChecker : MonoBehaviour
{
    public static WordChecker Inst;

    enum WordState { None, Valid, Invalid}
    WordState wordState = WordState.None;

    public string word="";

    public TextAsset[] wordDictionaries;
    public LetterTileSlot[] tileSlots;

    public Transform foundWordList;
    public GameObject foundWordPrefab;

    public TMP_Text scoreTxt,feedbackTxt;

    private Vector3 targetColor=new(.5f,.5f,.5f);
    private SpriteRenderer _sprRender;

    public List<string> playedWords = new();
    string errorMsg;
    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _sprRender = GetComponent<SpriteRenderer>();
        feedbackTxtShadow = feedbackTxt.gameObject.transform.parent.gameObject;
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

        targetScore = (MinigameManager.selectedDifficulty) switch { GameDifficulty.Easy => 15, GameDifficulty.Normal => 30, GameDifficulty.Hard => 60, _ => 15 };
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
                SetFeedbackTxt(errorMsg);
                break;
            case WordState.Invalid:
                //Give "not a word" message
                SetFeedbackTxt(errorMsg);
                //DropAllTiles();
                break;
            case WordState.Valid:
                playedWords.Add(word);
                int scoreInc = (word.Length) switch { 3 => 3, 4 => 4, 5 => 6, 6 => 8, 7 => 10, 8 => 12, _ => 3 };
                score += scoreInc;
                SetFeedbackTxt("+" + scoreInc + " points!");
                scoreTxt.text = "Score: "+score;
                GameObject _word = Instantiate(foundWordPrefab,foundWordList);
                _word.GetComponent<TMP_Text>().text = word;
                DropAllTiles();
                CheckEndGame();
                break;
        }
        if(textDisappearRoutine!=null)StopCoroutine(textDisappearRoutine);
        textDisappearRoutine = StartCoroutine(textDisappear());
    }

    int targetScore;
    private void CheckEndGame()
    {
        if (score >= targetScore) MenuManager.DelayAction(0.6f, () => { MenuManager.Inst.ChangeScene("WinScreen"); });
    }
    private void DropAllTiles()
    {
        for (int i = 0; i < tileSlots.Length; i++)
        {
            if (tileSlots[i].heldTile != null)
            {
                //tileSlots[i].heldTile.tweenPos.SetPositionX(0);
                //tileSlots[i].heldTile.tweenPos.SetPositionY(0);
                LetterTile tile = tileSlots[i].heldTile;
                //Tween tile back to letter tile jumble
                tile.tweenPos.SetPositionX(Random.Range(-8.25f, 4.6f));
                tile.tweenPos.SetPositionY(Random.Range(-4.5f, 2.5f));
                tile.tweenPos.rate = 0.05f;
                MenuManager.DelayAction(1, () => { tile.tweenPos.rate = .2f; tile.tweenPos.SetPosition(tile.transform); tile.enabled = true; });
                //Drop tile
                tileSlots[i].DropTile(tile);
                tile.ClearCollisions();
                tile.enabled = false;
            }
            else break;
        }
        checkSlotsShown();
        LetterTile[] tiles = FindObjectsOfType<LetterTile>();
        foreach (LetterTile t in tiles)
        {
            //t.tweenPos.SetPositionX(Random.Range(-8.25f, 4.6f));
            //t.tweenPos.SetPositionY(Random.Range(-4.5f, 2.5f));
        }
    }
    private GameObject feedbackTxtShadow;
    private void SetFeedbackTxt(string txt)
    {
        feedbackTxt.text = txt;
        feedbackTxtShadow.SetActive(txt!="");
    }
    private Coroutine textDisappearRoutine;
    IEnumerator textDisappear(float _t=3)
    {
        yield return new WaitForSecondsRealtime(_t);
        SetFeedbackTxt("");
    }

    public static void SetNextSlotTile(LetterTile tile){Inst.setNextSlotTile(tile);}
    public void setNextSlotTile(LetterTile tile)
    {
        for (int i = 0; i < tileSlots.Length; i++)
        {
            if (tileSlots[i].heldTile == null) { tileSlots[i].SetTile(tile); break; }
        }
    }
    public void CalculateTargetScore(int possibleScore)
    {
        targetScore = possibleScore;
        targetScore /= (MinigameManager.selectedDifficulty) switch { GameDifficulty.Easy => 50, GameDifficulty.Normal => 25, GameDifficulty.Hard => 10, _=>50};
        SetFeedbackTxt("Get a score of " + targetScore);
        if (textDisappearRoutine != null) StopCoroutine(textDisappearRoutine);
        textDisappearRoutine = StartCoroutine(textDisappear(5));
    }
}
