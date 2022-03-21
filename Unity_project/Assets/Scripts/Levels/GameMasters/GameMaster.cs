using System.Collections.Generic;
using Levels.DataManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace Levels
{

    public class GameMaster : MonoBehaviour
    {
        private string pathToCharacters = "Prefabs"; // The path to init prefabs
        [SerializeField] protected int startX = 0;
        [SerializeField] protected int startY = 0;

        public void Start()
        {
            // Load all infos of the level for this player
            LoadLevelInfos infos = FindObjectOfType<LoadLevelInfos>();
            if (infos != null)
            {
                GameObject prefab = Resources.Load<GameObject>($"{pathToCharacters}/Heros/{infos.hero.ToString()}");
                
                // DEBUG OPTION
                if (infos.debug)
                {
                    PersonnalDebug("Default character chosen: Kitsune");
                    prefab = Resources.Load<GameObject>($"{pathToCharacters}/Heros/Kitsune_weap"); // Load Kitsune by default (because it's my favorite one)
                }
                

                if (infos.multiplayer)
                {
                    PhotonNetwork.Instantiate($"{pathToCharacters}/Heros/{prefab.name}", new Vector3(startX, startY), Quaternion.identity);
                    gameObject.GetComponentInChildren<PauseMenu>().enabled = false;
                }
                else
                {
                    Instantiate(prefab, new Vector3(startX, startY), Quaternion.identity);
                    gameObject.GetComponentInChildren<PauseMenu>().enabled = true;
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
