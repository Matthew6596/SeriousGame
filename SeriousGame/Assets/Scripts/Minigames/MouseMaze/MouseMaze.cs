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
    Vector2 prevMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        MenuManager.lastMinigame = "MouseMaze";
        Mouse.current.WarpCursorPosition(mouseStartingPos);
    }

    // Update is called once per frame
    void Update()
    {
       // prevMousePosition = Camera.main.WorldToScreenPoint(PlayerMouse.inst.mousePos); //NEW
        /*(in update store mousepos in variable, on collide enter go to previous mouse pos. 
         * Current version sets mouse pos back to where the collision first happened)*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.GetComponent<MouseFollower>() != null) //if mouse
        {
            collision.gameObject.GetComponent<MouseFollower>().frozen = true;
            //mousePosition = prevMousePosition; //NEW
            //mousePosition = Camera.main.WorldToScreenPoint(PlayerMouse.inst.mousePos); //OLD

            Debug.Log("Mouse");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MouseFollower>() != null) //if mouse
        {
            collision.gameObject.GetComponent<MouseFollower>().frozen = true;
            //Mouse.current.WarpCursorPosition(mousePosition);
        }
    }

}
