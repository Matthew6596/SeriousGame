using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MouseMaze : MonoBehaviour
{
    //PlayerMouse
    public Vector2 mouseStartingPos = new Vector2(50, 550);
    Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        MenuManager.lastMinigame = "MouseMaze";
        Mouse.current.WarpCursorPosition(mouseStartingPos);
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
            mousePosition = Camera.main.WorldToScreenPoint(PlayerMouse.inst.mousePos);
            //position.y -= 1;

            //Mouse.current.WarpCursorPosition(position);
            //PlayerMouse.inst.mousePos = mouseStartingPos;
            Debug.Log("Mouse");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MouseFollower>() != null) //if mouse
        {
            Mouse.current.WarpCursorPosition(mousePosition);
        }
    }
}
