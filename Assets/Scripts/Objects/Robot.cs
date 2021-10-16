using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject UIManager;
    private GameObject soundManager;
   private GameObject RobotExplosion_Particle;
    private IEnumerator blowupCoroutine;

    void Start(){
        gameManager= GameObject.FindWithTag("GameManager");
        UIManager= GameObject.FindWithTag("UIManager");
        soundManager= GameObject.FindWithTag("SoundManager");
        RobotExplosion_Particle = transform.Find("RobotExplosion_Particle").gameObject;
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Garbage"){
            Destroy(other.gameObject);
            UIManager.SendMessage("ReduceRemainGarbages");
        }

        if(other.gameObject.tag == "HardObject"){
            soundManager.SendMessage("StopVacuumSound");
            gameManager.SendMessage("RobotCrashed");
            blowupCoroutine = WaitAndBlowup(1.261f);
            StartCoroutine(blowupCoroutine);
            SoundManager.sfxInstance.audioSource.PlayOneShot(SoundManager.sfxInstance.crashWarningSound,.5f);
        }
    }

    private IEnumerator WaitAndBlowup(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SoundManager.sfxInstance.audioSource.PlayOneShot(SoundManager.sfxInstance.blowupSound, .5f);
        yield return new WaitForSeconds(.1f);
        RobotExplosion_Particle.SetActive(true);
    }
}
