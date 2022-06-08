using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Levels.DataManager;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;

    public GameObject pauseMenu;
    public GameObject SettingMenu;
    private bool NotMulti;

    void Awake()
    {
        // Load if this players is only or not
        LoadLevelInfos infos = FindObjectOfType<LoadLevelInfos>();
        NotMulti = !infos.multiplayer;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()//desactive les menu + enlève la pause si en campagne
    {
        pauseMenu.SetActive(false);
        SettingMenu.SetActive(false);
        if (NotMulti)
        {
            Time.timeScale = 1;
        }
        IsGamePaused = false;
    }

    private void Pause()//active les menu + met en pause si en campagne
    {
        pauseMenu.SetActive(true);
        if (NotMulti)
        {
            Time.timeScale = 0;
        }
        IsGamePaused = true;
    }
    public void RestartLevel() //Restarts the level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void DisplaySettingMenu()
    {
        pauseMenu.SetActive(false);
        SettingMenu.SetActive(true);
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu-principal");
    }
}
