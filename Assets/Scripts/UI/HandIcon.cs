using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIcon : MonoBehaviour
{
    private float step = 1f;
    public int count;


    void FixedUpdate(){
        if(count<100){
        transform.position = new Vector3(transform.position.x + step,transform.position.y+ step*3,transform.position.z);
        this.GetComponent<CanvasRenderer>().SetColor(new Color(1.0f, 1.0f, 1.0f, (100f - (float)count)/100f));
        count++;
        }
    }


}
