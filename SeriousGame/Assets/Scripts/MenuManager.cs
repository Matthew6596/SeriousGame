using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Inst;
    //private GameManager gm;

    private void Awake()
    {
        Inst = this;
    }

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

    public static void DelayAction(float seconds,Action action)
    {
        Inst.StartCoroutine(Inst.delayAction(seconds, action));
    }
    private IEnumerator delayAction(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }
}
