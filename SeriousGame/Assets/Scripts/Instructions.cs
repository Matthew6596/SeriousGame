using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Instructions : MonoBehaviour
{
    public TMP_Text[] textBoxes;
    public Image[] images;

    public void SetText(TMP_Text textBox, string newText)
    {
        textBox.text = newText;
    }

    public void SetTextBoxActive(TMP_Text textBox, bool active)
    {
        textBox.gameObject.SetActive(active);
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
