using System.Collections;
using System.Collections.Generic;
using Levels.DataManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

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
                    firstHand = Resources.Load<GameObject>($"{pathToPrefabs}/Weapons/{infos.firstHand.ToString()}");
                    secondHand = Resources.Load<GameObject>($"{pathToPrefabs}/Weapons/{infos.secondHand.ToString()}");
                }


                if (infos.multiplayer)
                {
                    PhotonNetwork.Instantiate($"{pathToPrefabs}/Heros/{hero.name}", new Vector3(startX, startY),
                        Quaternion.identity);
                    gameObject.GetComponentInChildren<PauseMenu>().enabled = false;
                }
                else
                {
                    hero = Instantiate(hero, new Vector3(startX, startY), Quaternion.identity);
                    gameObject.GetComponentInChildren<PauseMenu>().enabled = true;
                }

                //firstHand.transform.parent = GameObject.Find($"{hero.name}/HandPosition").transform; // The / will go search in the children
                Instantiate(secondHand, hero.transform.Find("HandPosition"));
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
