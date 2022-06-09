using System;
using System.Collections;
using System.Collections.Generic;
using Levels.DataManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Playground.Characters.Heros;
using Playground.Weapons;
using Object = UnityEngine.Object;

namespace Levels
{

    public class GameMaster : MonoBehaviour
    {
        private string pathToPrefabs = "Prefabs"; // The path to init prefabs
        [SerializeField] protected int startX = 0;
        [SerializeField] protected int startY = 0;
        [SerializeField] protected HealthBar healthBar;
        public void Start()
        {

            // Load all infos of the level for this player
            LoadLevelInfos infos = FindObjectOfType<LoadLevelInfos>();
            if (infos != null)
            {
                GameObject hero = Resources.Load<GameObject>($"{pathToPrefabs}/Heros/{infos.hero.ToString()}");
                PersonnalDebug(infos.firstHand.ToString());
                GameObject firstHand =
                    Resources.Load<GameObject>($"{pathToPrefabs}/Weapons/{infos.firstHand.ToString()}");
                GameObject secondHand =
                    Resources.Load<GameObject>($"{pathToPrefabs}/Weapons/{infos.secondHand.ToString()}");
                

                // DEBUG OPTION THAT OVERRIDE THE DATAMANAGER
                if (infos.debug)
                {
                    
                    PersonnalDebug("Default character chosen: Kitsune");
                    hero = Resources.Load<GameObject>(
                        $"{pathToPrefabs}/Heros/Kitsune"); // Load Kitsune by default (because it's my favorite one)
                    firstHand = Resources.Load<GameObject>($"{pathToPrefabs}/Weapons/SmallSword");
                }

                GameObject heros;
                if (infos.multiplayer)
                {
                    if (NextScene.Players.Count > 0)
                    {
                        NextScene.Players.Clear();
                        
                        foreach (var aName in NextScene.PlayersNames)
                        {
                            heros = PhotonNetwork.Instantiate($"{pathToPrefabs}/Heros/{aName}", new Vector3(startX, startY), Quaternion.identity);
                            var weapon = PhotonNetwork.Instantiate($"{pathToPrefabs}/Weapons/{firstHand.name}",
                                new Vector3(startX, startY), Quaternion.identity);
                            weapon.transform.parent = heros.transform.Find("HandPosition");
                            heros.GetComponent<Hero>().SetupPrimatyWeapon(weapon);
                            gameObject.GetComponentInChildren<PauseMenu>().enabled = false;
                            heros.GetComponent<Hero>().SetupHealthBar(healthBar);
                            NextScene.Players.Add(heros);
                        }
                    }
                    else
                    {
                        heros = PhotonNetwork.Instantiate($"{pathToPrefabs}/Heros/{hero.name}", new Vector3(startX, startY),
                        Quaternion.identity);
                        var weapon = PhotonNetwork.Instantiate($"{pathToPrefabs}/Weapons/{firstHand.name}",
                            new Vector3(startX, startY), Quaternion.identity);
                        weapon.transform.parent = heros.transform.Find("HandPosition");
                        heros.GetComponent<Hero>().SetupPrimatyWeapon(weapon);
                        gameObject.GetComponentInChildren<PauseMenu>().enabled = false;
                        heros.GetComponent<Hero>().SetupHealthBar(healthBar);
                    }
                    //heros = PhotonNetwork.Instantiate($"{pathToPrefabs}/Heros/{hero.name}", new Vector3(startX, startY),
                        //Quaternion.identity);
                        
                }
                else
                {
                    heros = Instantiate(hero, new Vector3(startX, startY), Quaternion.identity);
                    var weapon = Instantiate(firstHand, heros.transform.Find("HandPosition"));
                    heros.GetComponent<Hero>().SetupPrimatyWeapon(weapon);
                    gameObject.GetComponentInChildren<PauseMenu>().enabled = true;
                    heros.GetComponent<Hero>().SetupHealthBar(healthBar);

                }

                //heros.GetComponent<Hero>().SetupHealthBar(healthBar);
            }
            else
            {
                Debug.LogError("Datamanager object not found, can't initialize the level");
            }
        }

        public static void PersonnalDebug(string message)
        {
            Debug.Log($"<color=orange>DEBUG LOG MESSAGE:</color> {message}");
        }
    }
}
