using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class NextScene : MonoBehaviour
    {
        public Animator transition;
        public string scene;
        public static List<GameObject> Players = new List<GameObject>();
        public static List<string> PlayersNames = new List<string>();
        
        void OnTriggerEnter2D(Collider2D obj)
        {
            if (obj.CompareTag("Heros") && GameObject.FindObjectOfType<GameMaster>().CanPassToNextLevel)
            {
                Players.Clear();
                foreach (var hero in GameObject.FindGameObjectsWithTag("Heros"))
                {
                    PlayersNames.Add(hero.name);
                }
                if (Physics2D.gravity.y > 0)
                    Physics2D.gravity = -Physics2D.gravity;
                
                if (SceneManager.GetActiveScene().buildIndex + 1 > PlayerPrefs.GetInt("reachedLevel", 0))
                {
                    PlayerPrefs.SetInt("reachedLevel", SceneManager.GetActiveScene().buildIndex + 1 );
                }
                StartCoroutine(LoadLevel(scene));
            }
        }

        [PunRPC]
        IEnumerator LoadLevel(string sceneName)
        {
            transition.SetTrigger("start");
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(sceneName);
        }
    }
}



