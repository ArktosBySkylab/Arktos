
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_menu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public static bool Gamelaunched = false;

    public GameObject PauseMenu;
    public GameObject MainMenu;
    public GameObject RestartButton;
    public GameObject startButton;
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
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsGamePaused = false;
    }

    private void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        IsGamePaused = true;
    }
    public void RestartLevel() //Restarts the level
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PlayCampain()
    {
        //SceneManager.LoadScene("Campain");
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        Gamelaunched = true;
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        Application.Quit();
    }

    public void DisplayMainMenu()
    {
        MainMenu.SetActive(true);
        startButton.SetActive(!Gamelaunched);
        RestartButton.SetActive(Gamelaunched);
    }
}
