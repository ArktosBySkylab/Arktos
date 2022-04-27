using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using Levels.DataManager;
using Playground.Characters.Heros;
using Playground.Weapons;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Main_Menu_Interaction : MonoBehaviour
{
    protected LoadLevelInfos dataManager;
    //information to save
    protected string HerosName;
    protected string WeaponsName;
    protected string LevelsName;
    protected float SoundsLevel; 
    private Image lastHeroImage;// use for character selection
    private Image lastWeaponImage;// use for weapon selection
    
    void Start()
    {
        dataManager = FindObjectOfType<LoadLevelInfos>();
        if (dataManager is null)
        {
            var data = Resources.Load<GameObject>("Prefabs/Scenes/Reference/DataManager");
            dataManager = Instantiate(data).GetComponent<LoadLevelInfos>();
            
            if (dataManager is null)
                Debug.LogError("DataManager not found");
            else
            {
                GetSavedInformation();
            }
        }
    }
    
    public void PlayCampain()
    {
        dataManager.multiplayer = false;
    }
    public void PlayMulti()
    {
        dataManager.multiplayer = true;
    }

    public void StartGame()
    {
        dataManager.LevelsName = LevelsName;
        dataManager.hero = getCharactaireTypeHerosNames();
        dataManager.firstHand = getWeaponTypeWeponNames();
        saveInformation();
        if (dataManager.multiplayer)
        {
            SceneManager.LoadScene("Loading");
        }
        else
        {
            SceneManager.LoadScene("Assets/Scenes/"+LevelsName+".unity");
        }
    }

    public void saveInformation()
    {
        PlayerPrefs.SetString("Character",HerosName);
        PlayerPrefs.SetString("Weapon",WeaponsName);
        PlayerPrefs.SetString("level",LevelsName);
        PlayerPrefs.SetFloat("sound",SoundsLevel);
    }
    public void GetSavedInformation()
    {
        HerosName = PlayerPrefs.GetString("Character","Kitsune");
        WeaponsName = PlayerPrefs.GetString("Weapon","SmallSword");
        LevelsName = PlayerPrefs.GetString("level","test");
        SoundsLevel = PlayerPrefs.GetFloat("sound",0.0f);
    }
    public void resetSavedInformation()
    {
        PlayerPrefs.DeleteAll();
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        saveInformation();
        Application.Quit();
    }
    public void getLevelsName(TextMeshProUGUI levelsname)//stoke dans name le nom du personnage selectionné à chaque nouveau choix
    {
        LevelsName = levelsname.text;
    }
    public void getHerosName(TextMeshProUGUI herosname)//stoke dans name le nom du personnage selectionné à chaque nouveau choix
    {
        HerosName = herosname.text;
    }

    public void markSelectedHero(Image image)
    {
        if (lastHeroImage != null)
        {
            lastHeroImage.enabled = false;
        }
        image.enabled = true;
        lastHeroImage = image;
    }
    
    public void getWeaponsName(TextMeshProUGUI weaponsname)//stoke dans name le nom du personnage selectionné à chaque nouveau choix
    {
        WeaponsName = weaponsname.text;
    }
    public void markSelectedWeapon(Image Weaponimage)
        {
            if (lastWeaponImage != null)
            {
                lastWeaponImage.enabled = false;
            }
            Weaponimage.enabled = true;
            lastWeaponImage = Weaponimage;
        }
    public HerosNames getCharactaireTypeHerosNames()//permet d'obtenir le type Herosname du personnage selectionné
    {
        switch (HerosName)// permet de covertir le string en type HerosNames
        {
            case "Kitsune":
                Debug.Log("getCharactaireTypeHerosNames Kitsune");
                return HerosNames.Kitsune;
            case "JojoTheKing":
                Debug.Log("getCharactaireTypeHerosNames JojoTheKing");
                return HerosNames.JojoTheKing;
            case "Ian":
                Debug.Log("getCharactaireTypeHerosNames Ian");
                return HerosNames.Ian;
            default:
                Debug.Log("getCharactaireTypeHerosNames default : kitsune");
                return HerosNames.Kitsune;
        }
        
    }
    public WeaponsNames getWeaponTypeWeponNames()//permet d'obtenir le type Herosname du personnage selectionné
    {
        switch (WeaponsName)// permet de covertir le string en type HerosNames
        {
            case "SmallSword":
                Debug.Log("getWeaponTypeWeponNames : SmallSword");
                return WeaponsNames.SmallSword;
            default:
                Debug.Log("getWeaponTypeWeponNames default : SmallSword");
                return WeaponsNames.SmallSword;
        }
        
    }
}
