using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using Levels.DataManager;
using Photon.Realtime;
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
    protected string HerosName;// save the name of the last played character
    protected string WeaponsName;// save the name of the last played weapon
    protected string LevelsName; // save the name of the last played level
    
    protected int reachedLevel; // save the last unlocked level
    protected int unlockedcharacter; // save the last unlocked character
    protected int unlockedwaepon; // save the last unlocked weapon
    //informatino to save
    private Image lastHeroImage;// use for character selection
    private Image lastWeaponImage;// use for weapon selection
    private string[] characterTab = new string[]{"Kitsune","JojoTheKing","Ian","Drow","Max"};
    private string[] levelTab = new string[]{"city","benetnash","mizar","alioth","megrez","phecda","merak","dubhe"};
    private string[] weaponTab = new string[]{"SmallSword"};
    void Awake()//peremt d'afficher au lancement du jeu le personnage choisit et son arme
        {
            GetSavedInformation();
            ShowUnlocked(unlockedcharacter,characterTab,"CharacterSelection/CharacterScroll/ButtonListViewPort/ButtonListContent/");
            ShowUnlocked(unlockedwaepon,weaponTab,"CharacterSelection/WeaponScroll/ButtonListViewPort/ButtonListContent/");
            ShowUnlocked(reachedLevel,levelTab,"LevelSelection/");
            markSelectedHero(GameObject.Find("CharacterSelection/CharacterScroll/ButtonListViewPort/ButtonListContent/" + HerosName).GetComponent<Image>());
            markSelectedWeapon(GameObject.Find("CharacterSelection/WeaponScroll/ButtonListViewPort/ButtonListContent/" + WeaponsName).GetComponent<Image>());
            GameObject.Find("CharacterSelection").SetActive(false);
            GameObject.Find("LevelSelection").SetActive(false);
        }
    void Start()
    {
        GameObject.Find("SETTING-MENU").SetActive(false); // permet de régler le son en fonction des playerpref
        dataManager = FindObjectOfType<LoadLevelInfos>();
        if (dataManager is null)
        {
            var data = Resources.Load<GameObject>("Prefabs/Scenes/Reference/DataManager");
            dataManager = Instantiate(data).GetComponent<LoadLevelInfos>();
            
            if (dataManager is null)
                Debug.LogError("DataManager not found");
        }
        Debug.Log(PlayerPrefs.GetInt("reachedLevel", -1));
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
            StartCoroutine(LoadLevel("Assets/Scenes/Loading.unity"));
        }
        else
        {
            StartCoroutine(LoadLevel("Assets/Scenes/"+LevelsName+".unity"));
        }
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
    public void saveInformation()
    {
        PlayerPrefs.SetString("Character",HerosName);
        PlayerPrefs.SetString("Weapon",WeaponsName);
        PlayerPrefs.SetString("level",LevelsName);
        
        PlayerPrefs.SetInt("reachedLevel",reachedLevel);
        PlayerPrefs.SetInt("unlockedcharacter",unlockedcharacter);
        PlayerPrefs.SetInt("unlockedwaepon",unlockedwaepon);
        
    }
    public void GetSavedInformation()
    {
        HerosName = PlayerPrefs.GetString("Character","Kitsune");
        WeaponsName = PlayerPrefs.GetString("Weapon","SmallSword");
        LevelsName = PlayerPrefs.GetString("level","city");
        
        reachedLevel = PlayerPrefs.GetInt("reachedLevel",0);//mettre à 0
        unlockedcharacter = PlayerPrefs.GetInt("unlockedcharacter",characterTab.Length - 1);//mettre à 0
        unlockedwaepon = PlayerPrefs.GetInt("unlockedwaepon",weaponTab.Length - 1);//mettre à 0
    }
    public void resetSavedInformation()
    {
        PlayerPrefs.DeleteAll();
        GetSavedInformation();
        saveInformation();
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        saveInformation();
        Application.Quit();
    }

    private void ShowUnlocked(int unloked ,string[] allelement, string path)
    {
        for (int i = unloked + 1; i < allelement.Length; i++)
        {
            GameObject.Find(path + allelement[i]).SetActive(false);
        }
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
                // Debug.Log("getCharactaireTypeHerosNames Kitsune");
                return HerosNames.Kitsune;
            case "JojoTheKing":
                // Debug.Log("getCharactaireTypeHerosNames JojoTheKing");
                return HerosNames.JojoTheKing;
            case "Ian":
                // Debug.Log("getCharactaireTypeHerosNames Ian");
                return HerosNames.Ian;
            case "Drow":
                // Debug.Log("getCharactaireTypeHerosNames Drow");
                return HerosNames.Drow;
            case "Max":
                // Debug.Log("getCharactaireTypeHerosNames Max");
                return HerosNames.Max;
            default:
                // Debug.Log("getCharactaireTypeHerosNames default : kitsune");
                return HerosNames.Kitsune;
        }
    }
    public WeaponsNames getWeaponTypeWeponNames()//permet d'obtenir le type Herosname du personnage selectionné
    {
        switch (WeaponsName)// permet de covertir le string en type HerosNames
        {
            case "SmallSword":
                // Debug.Log("getWeaponTypeWeponNames : SmallSword");
                return WeaponsNames.SmallSword;
            default:
                // Debug.Log("getWeaponTypeWeponNames default : SmallSword");
                return WeaponsNames.SmallSword;
        }
        
    }
}
