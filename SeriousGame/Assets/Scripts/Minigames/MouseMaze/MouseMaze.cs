using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMaze : MonoBehaviour
{
    //PlayerMouse
    public Vector2 mouseStartingPos = new Vector2(-8, 0);

    // Start is called before the first frame update
    void Start()
    {
        MenuManager.lastMinigame = "MouseMaze";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseCollision()
    {
        Debug.Log("Mouse");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.GetComponent<MouseFollower>() != null) //if mouse
        {
            Debug.Log("Mouse");
        }
    }
}
