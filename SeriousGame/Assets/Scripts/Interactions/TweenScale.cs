using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : MonoBehaviour
{
    public float rate=0.1f;
    private Vector3 targetScale, ogScale;

    // Start is called before the first frame update
    void Start()
    {
        ogScale = transform.localScale;
        targetScale = ogScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale=Tween.LazyTweenScaled(transform.localScale, targetScale, rate);
    }

    public void SetScaleX(float x){ targetScale.x = x; }
    public void SetScaleY(float y){ targetScale.y = y; }
    public void SetScaleZ(float z){ targetScale.z = z; }
    public void SetImmediateScaleX(float x) { transform.localScale = new(x,transform.localScale.y,transform.localScale.z); }
    public void SetImmediateScaleY(float y) { transform.localScale = new(transform.localScale.x, y,transform.localScale.z); }
    public void SetImmediateScaleZ(float z) { transform.localScale = new(transform.localScale.x, transform.localScale.y,z); }
    public void ResetScale(){targetScale = ogScale;}
    public void ResetScaleImmediate(){transform.localScale = ogScale;}
}
