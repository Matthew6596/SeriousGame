using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGoal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MouseFollower>() != null) 
        {
            MenuManager.DelayAction(1, () => { MenuManager.Inst.ChangeScene("Hub"); });
            Debug.Log("You Win!");
        }
    }
}