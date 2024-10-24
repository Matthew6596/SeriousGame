using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MouseInteractable : MonoBehaviour
{
    public UnityEvent onMouseEnter;
    public UnityEvent onMouseExit;
    public UnityEvent onMouseDown;
    public UnityEvent onMouseUp;

    Collider2D _collider;

    public bool mouseInside = false;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        PlayerMouse.interactables.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckInside(out bool mouseWasInside)
    {
        bool prevMouseInside = mouseInside;
        mouseInside = _collider.OverlapPoint(PlayerMouse.inst.mousePos);
        mouseWasInside = mouseInside;
        return (prevMouseInside != mouseInside);
    }
    
}
