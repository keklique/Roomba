using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEffects : MonoBehaviour
{
    [SerializeField]int interpolationFramesCount = 250;
    int elapsedFrames = 0;
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
    }

    void Update(){
        if(transform.localScale != Vector3.one){
        UITextAnimation();
        }
    }

    void UITextAnimation(){
        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
        Vector3 interpolatedPosition = Vector3.Lerp(Vector3.zero, Vector3.one, interpolationRatio);
        transform.localScale = interpolatedPosition;
        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
    }
}
