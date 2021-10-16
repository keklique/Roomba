using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonPersistent<UIManager>
{
    [SerializeField]private GameObject levelTextObject;
    [SerializeField]private GameObject progressionBar;
    private int initialGarbages;
    private int remainGarbages;
    public float progressionPercent;
    [SerializeField]private GameObject[] garbages;
    [SerializeField]private GameObject[] stars;
    [SerializeField]private GameObject batteryBar;
    [SerializeField]private int initialBatteryCapacity;
    [SerializeField]private int remainBatteryCapacity;
    [SerializeField]private float batteryPercent;
    private GameObject soundManager;
    private GameObject gameManager;

    [Header("End Level Menu")]
    [SerializeField]private GameObject nextLevelButton;
    [SerializeField]private GameObject tryAgainButton;
    [SerializeField]private GameObject mainMenuButton;
    [SerializeField]private GameObject levelFailedImage;
    [SerializeField]private GameObject levelCopmletedImage;


    void Start(){
        garbages = GameObject.FindGameObjectsWithTag("Garbage");
        initialGarbages = garbages.Length;
        remainGarbages = initialGarbages;
        soundManager = GameObject.FindWithTag("SoundManager");
        gameManager = GameObject.FindWithTag("GameManager");
    }

    void Update(){
        CalculateProgression();
        SetProgressionPercent();
        CheckStars();
        CalculateBattery();
        SetBatteryPercent();
    }

    void SetLevelText(int level){
        levelTextObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Level " + level.ToString();
    }

    void SetProgressionPercent(){
        progressionBar.GetComponent<Slider>().value = progressionPercent;
    }

    float CalculateProgression(){
        progressionPercent = ((float)initialGarbages - (float)remainGarbages)/(float)initialGarbages;
        return progressionPercent;
    }

    void ReduceRemainGarbages(){
        remainGarbages--;
    }

    void CheckStars(){
        if(progressionPercent>.5f && !stars[0].activeInHierarchy){
            stars[0].SetActive(true);
            SoundManager.sfxInstance.audioSource.PlayOneShot(SoundManager.sfxInstance.starSound, .5f);
        }
        if(progressionPercent>.725f && !stars[1].activeInHierarchy){
            stars[1].SetActive(true);
            SoundManager.sfxInstance.audioSource.PlayOneShot(SoundManager.sfxInstance.starSound,.5f);
        }
        if(progressionPercent>.945f && !stars[2].activeInHierarchy){
            stars[2].SetActive(true);
            SoundManager.sfxInstance.audioSource.PlayOneShot(SoundManager.sfxInstance.starSound,.5f);
        }
    }

    void SetInıtialBatteryCapacity(int tempBattery){
        initialBatteryCapacity = tempBattery;
        remainBatteryCapacity = tempBattery;
    }

    void CalculateBattery(){
        batteryPercent = 1 -((float)initialBatteryCapacity - (float)remainBatteryCapacity)/(float)initialBatteryCapacity;
    }

    void ReduceRemainBattery(){
        remainBatteryCapacity--;
    }
    void SetBatteryPercent(){
        batteryBar.GetComponent<Slider>().value = batteryPercent;
    }

    void LevelFailed(float waitTime){
        StartCoroutine(LevelFailedCoroutine( waitTime));
    }

    private IEnumerator LevelFailedCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        levelFailedImage.SetActive(true);
        tryAgainButton.SetActive(true);
        mainMenuButton.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SoundManager.sfxInstance.audioSource.PlayOneShot(SoundManager.sfxInstance.failedSound,.7f);
    }


    void LevelCompleted(){
        SoundManager.sfxInstance.audioSource.PlayOneShot(SoundManager.sfxInstance.victorySound,.5f);
        levelCopmletedImage.SetActive(true);
        nextLevelButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }
    public void TryAgain(){
        gameManager.SendMessage("ReloadLevel");
    }

    public void NextLevel(){
        gameManager.SendMessage("loadNextLevel");
    }

    public void MainMenu(){
        gameManager.SendMessage("MainMenu");
    }
}