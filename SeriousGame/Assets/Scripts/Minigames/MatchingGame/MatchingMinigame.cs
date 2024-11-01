using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameDifficulty { Easy, Normal, Hard}
public class MatchingMinigame : MonoBehaviour
{
    public static MatchingMinigame Inst;
    public static GameDifficulty difficulty=GameDifficulty.Easy;

    public GameObject cardPrefab;

    public Sprite[] cardSprites;
    public Transform[] cardPositionsEasy,cardPositionsNormal,cardPositionsHard;
    public float cardScaleEasy,cardScaleNormal,cardScaleHard;

    public List<MatchingCard> cards { get; set; } = new();

    private void Awake(){Inst = this;}

    // Start is called before the first frame update
    void Start()
    {
        //---TEMPORARY---
        float r = Random.value;
        if (r <= .33f)
        {
            difficulty = GameDifficulty.Easy;
        }
        else if (r < .67f)
        {
            difficulty=GameDifficulty.Normal;
        }
        else
        {
            difficulty=GameDifficulty.Hard;
        }
        //-------

        StartMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMinigame()
    {
        Transform[] cardPositions = (difficulty) switch
        {
            GameDifficulty.Easy => cardPositionsEasy,
            GameDifficulty.Normal => cardPositionsNormal,
            GameDifficulty.Hard => cardPositionsHard,
            _ => cardPositionsEasy,
        };
        float cardScale = (difficulty) switch
        {
            GameDifficulty.Easy => cardScaleEasy,
            GameDifficulty.Normal => cardScaleNormal,
            GameDifficulty.Hard => cardScaleHard,
            _ => cardScaleEasy,
        };
        MenuManager.lastMinigame = "MatchingGame";
        List<Transform> positions = new(); positions.AddRange(cardPositions);
        int numPairs = (cardPositions.Length/2)+1;
        for(int i=1, j=1; i<numPairs; i++, j++)
        {
            if (j >= cardSprites.Length) j = 1;

            //first card
            GameObject c1 = Instantiate(cardPrefab);
            c1.transform.localScale = Vector3.one * cardScale;
            c1.GetComponent<TweenScale>().SetOGScale(c1.transform.localScale);
            MatchingCard card1 = c1.GetComponent<MatchingCard>();
            card1.cardType = j;
            int r = Random.Range(0, positions.Count);
            card1.transform.position = positions[r].position; positions.RemoveAt(r);

            //it's matching card
            GameObject c2 = Instantiate(cardPrefab);
            c2.transform.localScale = Vector3.one * cardScale;
            c2.GetComponent<TweenScale>().SetOGScale(c2.transform.localScale);
            MatchingCard card2 = c2.GetComponent<MatchingCard>();
            card2.cardType = j;
            r = Random.Range(0, positions.Count);
            card2.transform.position = positions[r].position; positions.RemoveAt(r);

            //add cards to list
            cards.Add(card1);
            cards.Add(card2);
        }
    }

    private MatchingCard heldCard=null;
    public void CheckCard(MatchingCard card)
    {
        if (heldCard == null) //first card flipped
        {
            heldCard = card;
        }
        else //second card flipped, check for match
        {
            PlayerMouse.inst.disableClick = true;
            if (heldCard.cardType == card.cardType) //cards match!
            {
                MenuManager.DelayAction(1, () => 
                {
                    heldCard.gameObject.SetActive(false);
                    card.gameObject.SetActive(false);
                    cards.Remove(heldCard);
                    cards.Remove(card);
                    if (cards.Count == 0) WinMinigame();
                    heldCard = null;
                    PlayerMouse.inst.disableClick = false;
                });
            }
            else //cards dont match :(
            {
                MenuManager.DelayAction(1, () => 
                { 
                    heldCard.FlipCardOver();
                    card.FlipCardOver();
                    heldCard = null;
                    PlayerMouse.inst.disableClick = false;
                });
            }
        }
    }
    public void WinMinigame()
    {
        MenuManager.DelayAction(0.6f, () => { MenuManager.Inst.ChangeScene("WinScreen"); });
        Debug.Log("You Win!");
    }
}
