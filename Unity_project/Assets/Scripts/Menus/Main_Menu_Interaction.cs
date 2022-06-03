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
    public Animator transition;
    //information to save
    protected string HerosName;
    protected string WeaponsName;
    protected string LevelsName;
    protected float SoundsLevel; 
    private Image lastHeroImage;// use for character selection
    private Image lastWeaponImage;// use for weapon selection
    void Awake()//peremt d'afficher au lancement du jeu le personnage choisit et son arme
        {
            GetSavedInformation();
            markSelectedHero(GameObject.Find("CharacterSelection/CharacterScroll/ButtonListViewPort/ButtonListContent/" + HerosName).GetComponent<Image>());
            markSelectedWeapon(GameObject.Find("CharacterSelection/WeaponScroll/ButtonListViewPort/ButtonListContent/" + WeaponsName).GetComponent<Image>());
            GameObject.Find("CharacterSelection").SetActive(false);
        }
    void Start()
    {
        
        dataManager = FindObjectOfType<LoadLevelInfos>();
        if (dataManager is null)
        {
            var data = Resources.Load<GameObject>("Prefabs/Scenes/Reference/DataManager");
            dataManager = Instantiate(data).GetComponent<LoadLevelInfos>();
            
            if (dataManager is null)
                Debug.LogError("DataManager not found");
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
            StartCoroutine(LoadLevel("Assets/Scenes/"+LevelsName+".unity"));
        }
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("start");
        Debug.Log("animation should be play");
        yield return new WaitForSeconds(1);
        Debug.Log("animation should be finish");
        SceneManager.LoadScene(sceneName);
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
        LevelsName = PlayerPrefs.GetString("level","city");
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
            case "Drow":
                Debug.Log("getCharactaireTypeHerosNames Drow");
                return HerosNames.Drow;
            case "Max":
                Debug.Log("getCharactaireTypeHerosNames Max");
                return HerosNames.Max;
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
