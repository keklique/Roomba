using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonPersistent<SoundManager>
{
    public static SoundManager sfxInstance;
    public AudioSource audioSource;
    public AudioSource vacuumAudioSource;
    public AudioSource backgroundSource;
    public AudioClip vacuumIdleSound;
    public AudioClip vacuumVacuumSound;
    public AudioClip starSound;
    public AudioClip blowupSound;
    public AudioClip crashWarningSound;
    public AudioClip victorySound;
    public AudioClip failedSound;

    private bool music = true;
    private bool sound=true;

    void Start(){
        sfxInstance = this;
    }

    void PlayVacuumSound(){
        vacuumAudioSource.volume = 0.138f;
    }

    void StopVacuumSound(){
        vacuumAudioSource.volume = 0f;
    }

    void StopMusic(){
        backgroundSource.enabled = false;
    }

    void PlayMusic(){
        backgroundSource.enabled = true;
    }

    void StopSound(){
        audioSource.enabled = false;
        vacuumAudioSource.enabled = false;
    }

    void PlaySound(){
        audioSource.enabled = true;
        vacuumAudioSource.enabled = true;
    }

    

    

}
