using System;
using System.Collections;
using System.Collections.Generic;
using Levels.DataManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Playground.Characters.Heros;
using Playground.Weapons;

namespace Levels
{

    public class GameMaster : MonoBehaviour
    {
        private string pathToPrefabs = "Prefabs"; // The path to init prefabs
        [SerializeField] protected int startX = 0;
        [SerializeField] protected int startY = 0;
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

                if (infos.multiplayer)
                {
                    var heros = PhotonNetwork.Instantiate($"{pathToPrefabs}/Heros/{hero.name}", new Vector3(startX, startY),
                        Quaternion.identity);
                    var weapon = PhotonNetwork.Instantiate($"{pathToPrefabs}/Weapons/{firstHand.name}",
                        new Vector3(startX, startY), Quaternion.identity);
                    weapon.transform.parent = heros.transform.Find("HandPosition");
                    heros.GetComponent<Hero>().SetupPrimatyWeapon(weapon);
                }
                else
                {
                    var heros = Instantiate(hero, new Vector3(startX, startY), Quaternion.identity);
                    var weapon = Instantiate(firstHand, heros.transform.Find("HandPosition"));
                    heros.GetComponent<Hero>().SetupPrimatyWeapon(weapon);
                }
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
