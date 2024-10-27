using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void QuitGame() { Application.Quit(); }

    public void ChangeScene(string sceneName)
    {
        PlayerMouse.interactables.Clear();
        SceneManager.LoadScene(sceneName);
    }

    private string selectedMinigameType;
    public void SelectMinigameType(string type)
    {
        selectedMinigameType = type;
        ChangeScene("MinigameSelect");
    }
}
