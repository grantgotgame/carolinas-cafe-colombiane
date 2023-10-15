using EasyTransition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroTransition : MonoBehaviour
{
    [SerializeField] Button StartGameButton;
    [SerializeField] Button OptionButton;
    [SerializeField] Button CreditsButton;
    [SerializeField] GameObject OptionPage;
    [SerializeField] GameObject MainPage;
    [SerializeField] TransitionSettings transitionSettings;


    private void Awake()
    {
        StartGameButton.onClick.AddListener(StartGame);
        OptionButton.onClick.AddListener(OptionPageActive);
        CreditsButton.onClick.AddListener(MainPageActive);
    }

    private void MainPageActive()
    {
        if (OptionPage != null && MainPage != null)
        {
            OptionPage.SetActive(false);
            MainPage.SetActive(true);
        }
        
    }

    private void OptionPageActive()
    {
        if(OptionPage!=null && MainPage != null)
        {
            OptionPage.SetActive(true);
            MainPage.SetActive(false);
        }
    }

    public void StartGame()
    {
        //   string _sceneName = NameFromIndex((SceneManager.GetActiveScene().buildIndex) + 1);
        string _sceneName = NameFromIndex(1);
        TransitionManager.Instance().Transition(_sceneName, transitionSettings, 0);
    }

    public void StartHardcore()
    {
        //TODO trigger reloadLevel or restart game
        PlayerPrefs.SetInt("HardcoreCheck", 1);
        //PlayerPrefs.SetInt("TemporaryScore", 0);
        //PlayerPrefs.SetInt("CurrentScore", 0);
        string _sceneName = NameFromIndex(3);
        TransitionManager.Instance().Transition(_sceneName, transitionSettings, 0.5f);
    }

    public void StartAdventure()
    {
        //TODO trigger reloadLevel or restart game
        string _sceneName = NameFromIndex(3);
        PlayerPrefs.SetInt("HardcoreCheck", 0);
        //PlayerPrefs.SetInt("TemporaryScore", 0);
        //PlayerPrefs.SetInt("CurrentScore", 0);
        TransitionManager.Instance().Transition(_sceneName, transitionSettings, 0.5f);
    }

    public void MainMenu()
    {
        //TODO trigger reloadLevel or restart game
        string _sceneName = NameFromIndex(0);
        //PlayerPrefs.SetInt("TemporaryScore", 0);
        //PlayerPrefs.SetInt("CurrentScore", 0);
        TransitionManager.Instance().Transition(_sceneName, transitionSettings, 0);
    }


    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

}
