using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterGeneration : MonoBehaviour
{
    private string _constCommon = "dhlnrst";
    private string _constRare = "bcfgkmpvwy";
    private string _constExtraRare = "jqxz";
    private string _vowel = "aeiouy";
    public GameObject tilePrefab;
    public int letterCount;
    public TMPro.TMP_Text _txt;

    WordChecker wordChecker;

    // Start is called before the first frame update
    void Start()
    {
        wordChecker = GetComponent<WordChecker>();

        int numPossible = 0;
        string letters=generateRandLetters();
        char[] _letters = letters.ToCharArray();
        foreach (TextAsset dictionary in wordChecker.wordDictionaries)
        {
            string[] words = dictionary.text.Split('\n', '\r');
            foreach (string word in words)
            {
                if (checkWordPossible(_letters,word))
                {
                    numPossible++;
                }
            }
        }
        _txt.text = ""+numPossible;

        foreach (char l in letters)
        {
            GameObject tile = Instantiate(tilePrefab);
            tile.GetComponent<LetterTile>().letter = char.ToUpper(l);
            Collider2D col = tile.GetComponent<Collider2D>();
            int ctr = 0;
            do
            {
                List<Collider2D> _cols = new();
                col.OverlapCollider(new(), _cols);
                tile.transform.position = new Vector3(UnityEngine.Random.Range(-8,8), UnityEngine.Random.Range(-4.5f, 2.5f), 0);
            } while (ctr > 0);
            tile.SetActive(true);
        }
    }

    private string generateRandLetters()
    {
        string _letters = "";
        //3-4 random vowels
        int numVowel = UnityEngine.Random.Range((int)3, 5);
        for (int i = 0; i < numVowel; i++)
        {
            //no repeat vowels
            char l = _vowel[UnityEngine.Random.Range(0, _vowel.Length)];
            while(_letters.Contains(l)) l = _vowel[UnityEngine.Random.Range(0, _vowel.Length)];
            _letters += l;
        }
        for (int i = numVowel; i < letterCount; i++)
        {
            float r = UnityEngine.Random.value;
            char l = _constCommon[UnityEngine.Random.Range(0, _constCommon.Length)];
            if (r <= .4f) l = _constRare[UnityEngine.Random.Range(0, _constRare.Length)];
            else if (r <= .1f) l = _constExtraRare[UnityEngine.Random.Range(0, _constExtraRare.Length)];

            _letters += l;
        }
        return _letters;
    }
    private bool checkWordPossible(char[] letters,string word)
    {
        List<char> letterList = new(); letterList.AddRange(letters);
        foreach(char _c in word)
        {
            if (letterList.Contains(_c)) letterList.Remove(_c);
            else return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
