using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPosition : MonoBehaviour
{
    public float rate=0.1f;
    private Vector3 targetPos, ogPos;

    // Start is called before the first frame update
    void Start()
    {
        ogPos = transform.localPosition;
        targetPos = ogPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Tween.LazyTweenScaled(transform.localPosition, targetPos, rate);
    }

    public void SetPosition(Transform _transform) { targetPos = _transform.position; }
    public void SetImmediatePos(Transform _transform) { transform.position = _transform.position; }
    public void SetPositionX(float x){ targetPos.x = x; }
    public void SetPositionY(float y){ targetPos.y = y; }
    public void SetPositionZ(float z){ targetPos.z = z; }
    public void SetImmediatePosX(float x) { transform.localPosition = new(x,transform.localPosition.y,transform.localPosition.z); }
    public void SetImmediatePosY(float y) { transform.localPosition = new(transform.localPosition.x, y,transform.localPosition.z); }
    public void SetImmediatePosZ(float z) { transform.localPosition = new(transform.localPosition.x, transform.localPosition.y,z); }
    public void MovePositionX(float x) { targetPos.x = transform.localPosition.x+x; }
    public void MovePositionY(float y) { targetPos.y = transform.localPosition.y+y; }
    public void ResetPosition(){ targetPos = ogPos; }
    public void ResetPositionImmediate(){transform.localPosition = ogPos; }
}
