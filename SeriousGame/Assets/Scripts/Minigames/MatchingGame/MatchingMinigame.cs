using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchingMinigame : MonoBehaviour
{
    public static MatchingMinigame Inst;

    public GameObject cardPrefab;

    public Sprite[] cardSprites;
    public Transform[] cardPositions;

    public List<MatchingCard> cards;

    private void Awake(){Inst = this;}

    // Start is called before the first frame update
    void Start()
    {
        StartMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMinigame()
    {
        List<Transform> positions = new(); positions.AddRange(cardPositions);
        for(int i=1; i<cardSprites.Length; i++)
        {
            //first card
            GameObject c1 = Instantiate(cardPrefab);
            MatchingCard card1 = c1.GetComponent<MatchingCard>();
            card1.cardType = i;
            int r = Random.Range(0, positions.Count);
            card1.transform.position = positions[r].position; positions.RemoveAt(r);

            //it's matching card
            GameObject c2 = Instantiate(cardPrefab);
            MatchingCard card2 = c2.GetComponent<MatchingCard>();
            card2.cardType = i;
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
        Debug.Log("You Win!");
    }
}
