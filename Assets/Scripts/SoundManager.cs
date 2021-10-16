using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonPersistent<SoundManager>
{
    public static SoundManager sfxInstance;
    public AudioSource audioSource;
    public AudioSource vacuumAudioSource;
    public AudioClip vacuumIdleSound;
    public AudioClip vacuumVacuumSound;
    public AudioClip starSound;
    public AudioClip blowupSound;
    public AudioClip crashWarningSound;
    public AudioClip victorySound;
    public AudioClip failedSound;

    void Start(){
        sfxInstance = this;
    }

    void PlayVacuumSound(){
        vacuumAudioSource.volume = 0.138f;
    }

    void StopVacuumSound(){
        vacuumAudioSource.volume = 0f;
    }

    

}
