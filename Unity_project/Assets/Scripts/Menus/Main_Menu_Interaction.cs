using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Levels.DataManager;
using Playground.Characters.Heros;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Main_Menu_Interaction : MonoBehaviour
{
    protected LoadLevelInfos dataManager;
    protected string name = "kitsune";// our default choise
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
        dataManager.hero = getCharactaireTypeHerosNames();
        SceneManager.LoadScene("testing_scene");
    }
    public void PlayMulti()
    {
        dataManager.multiplayer = true;
        dataManager.hero = getCharactaireTypeHerosNames();
        SceneManager.LoadScene("Loading");
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        Application.Quit();
    }
    public void getCharactaireName(TextMeshProUGUI herosname)//stoke dans name le nom du personnage selectionné à chaque nouveau choix
    {
         
        Debug.Log("dropdown start");
        name = herosname.text;
        Debug.Log(name);
        Debug.Log("dropdown end");
    }
    
    public HerosNames getCharactaireTypeHerosNames()//permet d'obtenir le type Herosname du personnage selectionné
        //j'ai pas trouvé mieux qu'un switch case 
    {
        switch (name)// permet de covertir le string en type HerosNames
        {
            case "kitsune":
                Debug.Log("getCharactaireTypeHerosNames kitsune");
                return HerosNames.Kitsune;
            case "JojoTheKing":
                Debug.Log("getCharactaireTypeHerosNames JojoTheKing");
                return HerosNames.JojoTheKing;
            default:
                Debug.Log("getCharactaireTypeHerosNames default : kitsune");
                return HerosNames.Kitsune;
        }
        
    }
}
