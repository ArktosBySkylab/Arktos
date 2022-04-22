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
    public static bool Gamelaunched = false;

    public GameObject pauseMenu;
    public GameObject SettingMenu;
    private bool multiplaying;

    void start()
    {
        LoadLevelInfos infos = FindObjectOfType<LoadLevelInfos>();
        multiplaying = infos.multiplayer;
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
                Pause(multiplaying);
            }
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsGamePaused = false;
    }

    private void Pause(bool is_mulptiplaying)
    {
        pauseMenu.SetActive(true);
        if (is_mulptiplaying)
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
        //SceneManager.LoadScene("Campain");
        pauseMenu.SetActive(false);
        SettingMenu.SetActive(true);
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("menu-principal");
    }
}
