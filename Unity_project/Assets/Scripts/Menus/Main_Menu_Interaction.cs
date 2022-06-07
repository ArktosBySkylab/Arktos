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
    
    protected int levelunlock; // save the last unlocked level
    protected int persounlock; // save the last unlocked character
    protected int waeponunlock; // save the last unlocked weapon
    //informatino to save
    private Image lastHeroImage;// use for character selection
    private Image lastWeaponImage;// use for weapon selection
    private string[] characterTab = new string[]{"Kitsune","JojoTheKing","Ian","Drow","Max"};
    private string[] levelTab = new string[]{"city","mazar"};
    private string[] weaponTab = new string[]{"SmallSword"};
    void Awake()//peremt d'afficher au lancement du jeu le personnage choisit et son arme
        {
            GetSavedInformation();
            
            ShowUnlockedcharacter(persounlock);
            ShowUnlockedWeapon(waeponunlock);
            markSelectedHero(GameObject.Find("CharacterSelection/CharacterScroll/ButtonListViewPort/ButtonListContent/" + HerosName).GetComponent<Image>());
            markSelectedWeapon(GameObject.Find("CharacterSelection/WeaponScroll/ButtonListViewPort/ButtonListContent/" + WeaponsName).GetComponent<Image>());
            GameObject.Find("CharacterSelection").SetActive(false);
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
        
        PlayerPrefs.SetInt("levelunlock",levelunlock);
        PlayerPrefs.SetInt("persounlock",persounlock);  
    }
    public void GetSavedInformation()
    {
        HerosName = PlayerPrefs.GetString("Character","Kitsune");
        WeaponsName = PlayerPrefs.GetString("Weapon","SmallSword");
        LevelsName = PlayerPrefs.GetString("level","city");
        
        levelunlock = PlayerPrefs.GetInt("levelunlock",levelTab.Length - 1);//mettre à 0
        persounlock = PlayerPrefs.GetInt("persounlock",characterTab.Length - 1);// mettre à 0
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

    private void ShowUnlockedcharacter(int lastCharacterId)
    {
        int i = 0;
        for (; i <= lastCharacterId; i++)
        {
            GameObject.Find("CharacterSelection/CharacterScroll/ButtonListViewPort/ButtonListContent/" + characterTab[i]).SetActive(true);
        }

        for (; i < characterTab.Length; i++)
        {
            GameObject.Find("CharacterSelection/CharacterScroll/ButtonListViewPort/ButtonListContent/" + characterTab[i]).SetActive(false);
        }
    }
    private void ShowUnlockedWeapon(int lastWeaponId)
    {
        int i = 0;
        for (; i <= lastWeaponId; i++)
        {
            GameObject.Find("CharacterSelection/WeaponScroll/ButtonListViewPort/ButtonListContent/" + weaponTab[i]).SetActive(true);
        }

        for (; i < weaponTab.Length; i++)
        {
            GameObject.Find("CharacterSelection/WeaponScroll/ButtonListViewPort/ButtonListContent/" + weaponTab[i]).SetActive(false);
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
