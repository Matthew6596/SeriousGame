using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Inst;

    public static string selectedMinigameCategory="Memory";
    public static string currMinigame;
    public static GameDifficulty selectedDifficulty;

    public int currMinigameIndex=0;

    public TMP_Text instructionsTxt;
    public GameObject nextGameBtn, prevGameBtn;

    public TextAsset[] minigameInstructions;
    public GameObject[] minigamePlayIcons;

    public TextAsset[] minigameCategories;

    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Filter out minigames by category
        List<GameObject> _playIcons = new();
        List<TextAsset> _instructions = new();
        string _minigamesInCategory="";
        foreach(TextAsset txt in minigameCategories)
        {
            //Get list of minigames for the selected category
            if(txt.name==selectedMinigameCategory)_minigamesInCategory = txt.text;
        }
        for (int i = 0; i < minigameInstructions.Length; i++)
        {
            //Only keep minigame things if in current chosen category
            if (_minigamesInCategory.Contains(minigameInstructions[i].name))
            {
                _playIcons.Add(minigamePlayIcons[i]);
                _instructions.Add(minigameInstructions[i]);
            }
        }
        minigameInstructions = _instructions.ToArray();
        minigamePlayIcons = _playIcons.ToArray();
        //end of minigame category filtering

        if (minigameInstructions.Length == 1) { prevGameBtn.SetActive(false); nextGameBtn.SetActive(false); }

        //Set instructions for the current minigame
        instructionsTxt.text = minigameInstructions[currMinigameIndex].text;
        minigamePlayIcons[currMinigameIndex].SetActive(true);
    }

    public void SetMinigame(int index)
    {
        if (index >= minigameInstructions.Length || index < 0) return;

        minigamePlayIcons[currMinigameIndex].SetActive(false); //hide old play button
        //Set new index and stuff
        currMinigameIndex = index;
        currMinigame = minigameInstructions[currMinigameIndex].name;
        //Set new instructions and show new play button
        instructionsTxt.text = minigameInstructions[currMinigameIndex].text;
        minigamePlayIcons[currMinigameIndex].SetActive(true);
    }
    public void SetMinigameBy(int increment) { SetMinigame(currMinigameIndex + increment); }

    public void SelectDifficulty(int diff)
    {
        selectedDifficulty = (GameDifficulty)diff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
