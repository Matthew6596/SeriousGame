using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    public UnityEvent Next;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.name == "Circle")
        {
            collision.GetComponent<MouseDraggable>().dragging = false;
            collision.GetComponent<MouseDraggable>().enabled = false;
            MenuManager.DelayAction(0.6f, () => { Next.Invoke(); });
        }
    }
}
