using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Interaction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayCampain()
    {
        //SceneManager.LoadScene("Campain");
        SceneManager.LoadScene("testing_scene_tmp");
    }
    public void PlayMulti()
    {
        //SceneManager.LoadScene("Campain");
        SceneManager.LoadScene("Loading");
    }
    public void closegame()//ne marche pas sur Unity (uniquement quand le jeu et lancé)
    {
        Application.Quit();
    }
}
