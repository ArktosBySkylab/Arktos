using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_menu : MonoBehaviour
{
    public static bool IsGamePaused = false;

    public GameObject PauseMenu;
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
        SceneManager.LoadScene("Campain");
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        Application.Quit();
    }
}
