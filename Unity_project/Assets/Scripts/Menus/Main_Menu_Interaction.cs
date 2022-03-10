using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Levels.DataManager;
using Playground.Characters.Heros;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Interaction : MonoBehaviour
{
    protected LoadLevelInfos dataManager;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = FindObjectOfType<LoadLevelInfos>();
        if (dataManager is null)
        {
            Debug.LogError("MainMenu: Data manager not found");
        }
    }
    
    public void PlayCampain()
    {
        //SceneManager.LoadScene("Campain");
        dataManager.multiplayer = false;
        SceneManager.LoadScene("testing_scene");
    }
    public void PlayMulti()
    {
        //SceneManager.LoadScene("Campain");
        dataManager.multiplayer = true;
        SceneManager.LoadScene("Loading");
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        Application.Quit();
    }
}
