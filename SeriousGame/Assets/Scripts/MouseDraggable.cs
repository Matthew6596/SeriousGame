using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TweenPosition))]
public class MouseDraggable : MouseInteractable
{
    private Vector2 clickOffset;
    public bool dragging = false;
    TweenPosition tweenPos;

    // Start is called before the first frame update
    void Start()
    {
        start();
        onMouseDown.AddListener(() => { dragging = true; clickOffset = PlayerMouse.MousePos - (Vector2)transform.position; });
        PlayerMouse.inst.onMouseUp.AddListener(() => { dragging = false; });
        tweenPos = GetComponent<TweenPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Vector2 targetPos = PlayerMouse.MousePos + clickOffset;
            tweenPos.SetPositionX(targetPos.x);
            tweenPos.SetPositionY(targetPos.y);
        }
    }
}
