using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq;

public class WordChecker : MonoBehaviour
{
    public TextAsset[] wordDictionaries;
    public LetterTileSlot[] tileSlots;

    private Vector3 targetColor;
    private SpriteRenderer _sprRender;

    // Start is called before the first frame update
    void Start()
    {
        _sprRender = GetComponent<SpriteRenderer>();
        foreach (LetterTileSlot slot in tileSlots) slot.onLetterEntered += (s,e)=> { CheckForWord(); };
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 col = Tween.LazyTweenScaled(new(_sprRender.color.r, _sprRender.color.g, _sprRender.color.b), targetColor, 0.1f);
        _sprRender.color = new Color(col.x,col.y,col.z);
    }

    public void CheckForWord()
    {
        string word = "";
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
        CheckDictionary(word.ToLower());
    }

    private void CheckDictionary(string word)
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

        if (words.Contains(word))
        {
            ValidWord();
        }
        else
        {
            InvalidWord();
        }
    }

    private void IncompleteWord()
    {
        targetColor = new(.5f, .5f, .5f);
    }
    private void InvalidWord()
    {
        targetColor = new(1, 0, 0);
    }
    private void ValidWord()
    {
        targetColor = new(0, 1, 0);
    }
}
