using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public static bool Gamelaunched = false;

    public GameObject PauseMenu;
    public GameObject SettingMenu;
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
        Debug.Log("hello Resume");
        PauseMenu.SetActive(false);
        Debug.Log("hello Resume");
        Time.timeScale = 1;
        Debug.Log("hello Resume");
        IsGamePaused = false;
        Debug.Log("hello Resume");
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
    public void DisplaySettingMenu()
    {
        //SceneManager.LoadScene("Campain");
        PauseMenu.SetActive(false);
        SettingMenu.SetActive(true);
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        Application.Quit();
    }
}
