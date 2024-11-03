using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterTile : MonoBehaviour
{
    private char _letter;
    public char letter { get => _letter; set
        {
            if(letterLabel==null) letterLabel = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            letterLabel.text = value.ToString();
            _letter = value;
        }
    }

    TMP_Text letterLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
