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
    protected string HerosName = "kitsune";// our default choise
    protected string WeaponsName = "SmallSword";// default (and only) choise
    private Image lastHeroImage;
    private Image lastWeaponImage;
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
                Debug.Log("before save");
                Debug.Log(HerosName);
                Debug.Log(WeaponsName);
                GetSavedInformation();
                Debug.Log("after save");
                Debug.Log(HerosName);
                Debug.Log(WeaponsName);
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
        dataManager.hero = getCharactaireTypeHerosNames();
        dataManager.firstHand = getWeaponTypeWeponNames();
        saveInformation(HerosName,WeaponsName);
        if (dataManager.multiplayer)
        {
            SceneManager.LoadScene("Loading");
        }
        else
        {
            SceneManager.LoadScene("Assets/Scenes/city.unity");
        }
    }

    public void saveInformation(string herosName, string weaponsName)
    {
        Debug.Log("have been saved");
        Debug.Log(herosName);
        Debug.Log(weaponsName);
        PlayerPrefs.SetString("Character",herosName);
        PlayerPrefs.SetString("Weapon",weaponsName);
    }
    public void GetSavedInformation()
    {
        HerosName = PlayerPrefs.GetString("Character");
        WeaponsName = PlayerPrefs.GetString("Weapon");
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        Application.Quit();
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
        
    }public WeaponsNames getWeaponTypeWeponNames()//permet d'obtenir le type Herosname du personnage selectionné
        //j'ai pas trouvé mieux qu'un switch case 
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
