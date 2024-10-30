using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Instructions : MonoBehaviour
{
    public GameObject[] textGroups;
    public Image[] images;

    //Trying to do stuff
    //public void SetText(int i, string newText)
    //{
    //    textBoxes[i].text = newText;
    //}

    public void SetTextBoxInactive(int i)
    {
        textGroups[i].SetActive(false);
    }

    public void SetTextBoxActive(int i)
    {
        textGroups[i].SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
