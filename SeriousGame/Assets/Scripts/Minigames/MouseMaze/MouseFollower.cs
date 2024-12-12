using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    public bool frozen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!frozen)
        {
            transform.position = PlayerMouse.inst.mousePos;
        }
    }

    public void SetFrozen(bool state)
    {
        frozen = state;
    }

}
