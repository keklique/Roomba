using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDeactive : MonoBehaviour
{
    [SerializeField]private float waitTime = 2f;

    void OnEnable(){

        StartCoroutine(SetDeactiveAfterWait(waitTime));
    }

    private IEnumerator SetDeactiveAfterWait(float waitTime)
    {
        //soundManager play warning sound
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }
}
