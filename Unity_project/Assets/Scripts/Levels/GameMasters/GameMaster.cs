using System.Collections.Generic;
using Levels.DataManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace Levels
{

    public class GameMaster : MonoBehaviour
    {
        private string pathToCharacters = "Prefabs";
        //[SerializeField] protected List<GameObject> players;
        [SerializeField] protected int startX = 0;
        [SerializeField] protected int startY = 0;

        public void Start()
        {
            //foreach (GameObject player in players)
            //{
            //    Instantiate(player, new Vector3(startCoords.Item1, startCoords.Item2), Quaternion.identity).SetActive(true);
            //}
            
            // TEMPORARY PART (pour la premiere soutenance uniquement)
            //GameObject character = Resources.Load<GameObject>("Prefabs/Heros/Kitsune");
            //Instantiate(character, new Vector3(startX, startY), Quaternion.identity).SetActive(true);
            LoadLevelInfos infos = FindObjectOfType<LoadLevelInfos>();
            if (infos != null)
            {
                GameObject prefab = Resources.Load<GameObject>($"{pathToCharacters}/Heros/{infos.hero.ToString()}");
                
                // DEBUG OPTION
                if (infos.debug && prefab is null)
                {
                    infos.PersonnalDebug("Default character chosen: Kitsune");
                    prefab = Resources.Load<GameObject>($"{pathToCharacters}/Heros/Kitsune"); // Load Kitsune by default (because it's my favorite one)
                }
                

                if (infos.multiplayer)
                {
                    PhotonNetwork.Instantiate($"{pathToCharacters}/Heros/{prefab.name}", new Vector3(startX, startY), Quaternion.identity);
                }
                else
                {
                    Instantiate(prefab, new Vector3(startX, startY), Quaternion.identity);
                }
            }
        }
    }
}
