using System;
using System.Collections;
using System.Collections.Generic;
using Levels.DataManager;
using Photon.Pun;
using Playground.Characters.Monsters;
using UnityEngine;

namespace Levels
{
    public class SpawnPoints : MonoBehaviour
    {
        /// <summary>
        /// Store the type and the number of monsters in this spawn point
        /// can be an infinity spawn
        /// </summary>
        public Dictionary<MonstersNames, float> monsters;
        /// <summary>
        /// Time between two spawns in seconds
        /// </summary>
        public float gapSpawn;
        private bool alreadyActivated = false;
        private bool multiplayer;

        public void Start()
        {
            monsters = new Dictionary<MonstersNames, float>();
            multiplayer = FindObjectOfType<LoadLevelInfos>().multiplayer;
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (!alreadyActivated)
            {
                StartCoroutine(SpawnMonsters());
            }
            alreadyActivated = true;
        }

        /// <summary>
        /// Will stop make monsters spawing from that spawn point
        /// </summary>
        public void StopSpawning()
        {
            StopCoroutine(SpawnMonsters());
        }

        protected IEnumerator SpawnMonsters()
        {
            foreach (KeyValuePair<MonstersNames,float> pair in monsters)
            {
                float number = pair.Value;
                GameObject monster = Resources.Load<GameObject>($"Prefabs/Monsters/{pair.Key.ToString()}");
                
                while (number > 0)
                {
                    number--;
                    if (multiplayer)
                    {
                        PhotonNetwork.Instantiate($"Prefabs/Monsters/{pair.Key.ToString()}", gameObject.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(monster, gameObject.transform.position, Quaternion.identity);
                    }

                    yield return new WaitForSeconds(gapSpawn);
                }
            }
        }
    }
}