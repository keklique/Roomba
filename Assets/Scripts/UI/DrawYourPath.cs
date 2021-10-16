using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawYourPath : MonoBehaviour
{
    private float step = 1f;
    void OnEnable()
    {
        transform.localScale = new Vector3(transform.localScale.x,0f,transform.localScale.y); 
    }

    void Update()
    {
        if(transform.localScale.y<=.9f){
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y + step*Time.deltaTime,transform.localScale.y);
        }
    }
}
