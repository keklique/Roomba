using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SingletonPersistent<LevelManager>
{
    public int currentLevelIndex;
    private GameObject gameManager;
    private GameObject UIManager;

    void Start(){
        GetLastLevel();
        GetManagers();
    }

    void GetLastLevel(){
        //For get last played level, yet game starts from level1
        currentLevelIndex = 0;
    }
    void GetManagers(){
        UIManager= GameObject.FindWithTag("UIManager");
        gameManager= GameObject.FindWithTag("GameManager");
    }

    void LoadNextLevel(){
        currentLevelIndex++;
        SceneManager.LoadScene(currentLevelIndex, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void ReloadLevel(){
        SceneManager.LoadScene(currentLevelIndex, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;  
    }

    void StartGame(){
        if(currentLevelIndex==0){
            LoadNextLevel();
        }else{
            ReloadLevel();
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        gameManager.SendMessage("ResetVaraibles");
        UIManager.SendMessage("ResetUI");
        UIManager.SendMessage("SetLevelText",currentLevelIndex);
        if(currentLevelIndex==1){
            UIManager.SendMessage("ActivateTutorials");
        }
        UIManager.SendMessage("SetActiveMainMenu",false);
    }
}
