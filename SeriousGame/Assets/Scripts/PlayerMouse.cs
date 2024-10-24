using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMouse : MonoBehaviour
{
    public static PlayerMouse inst;

    public UnityEvent onMouseDown;
    public UnityEvent onMouseUp;

    public static Vector2 MousePos => inst.mousePos;
    public static bool MouseDown => inst.mouseDown;

    public static List<MouseInteractable> interactables=new();

    public bool mouseDown = false;
    public Vector2 mousePos;


    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseMove(InputAction.CallbackContext ctx)
    {
        if (Camera.main == null) return;

        mousePos = ctx.ReadValue<Vector2>();
        //convert mouse pos to world pos
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //check mouse collisions
        
        foreach (MouseInteractable i in interactables) 
        {
            if (i == null) { Debug.LogWarning("null intertactable in mouse list still"); break; }
            if (i.CheckInside(out bool inside))
            {
                if (inside) i.onMouseEnter.Invoke();
                else i.onMouseExit.Invoke();
            }
        }
    }
    public void MouseClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) //if clicked
        {
            mouseDown = true;
            onMouseDown.Invoke();
            //bool err = false;
            Action delayedEvents = () => { };
            foreach (MouseInteractable i in interactables)
            {
                if (i.mouseInside)
                {
                    if (i.IsMenuButton)
                        delayedEvents += () => { i.onMouseDown.Invoke(); };
                    else
                        i.onMouseDown.Invoke(); //invoke if clicked inside
                }
            }
            delayedEvents();
            //if (err) interactables.Clear();
        }
        else if (ctx.canceled)
        {
            mouseDown = false;
            onMouseUp.Invoke();
            //bool err = false;
            Action delayedEvents = () => { };
            foreach (MouseInteractable i in interactables)
            {
                if (i.mouseInside)
                {
                    if (i.IsMenuButton)
                        delayedEvents += () => { i.onMouseDown.Invoke(); };
                    else
                        i.onMouseDown.Invoke(); //invoke if clicked inside
                }
            }
            delayedEvents();
            //if (err) interactables.Clear();
        }
    }
}
